using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Arbor.App.Extensions.ExtensionMethods;
using Arbor.App.Extensions.Tasks;
using Arbor.KVConfiguration.Core;
using Arbor.Primitives;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Milou.Deployer.Web.Agent.Host.Configuration;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Milou.Deployer.Web.Agent.Host.Services
{
    public sealed class AgentService : BackgroundService, IAsyncDisposable
    {
        private readonly AgentConfiguration? _agentConfiguration;
        private readonly IDeploymentPackageAgent _deploymentPackageAgent;
        private readonly EnvironmentVariables _environmentVariables;
        private readonly IKeyValueConfiguration _keyValueConfiguration;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly ILogger _logger;
        private readonly LoggingLevelSwitch _loggingLevelSwitch;
        private readonly IMediator _mediator;
        private readonly List<IDisposable> _subscriptions = new();
        private AgentId? _agentId;
        private string _connectionUrl = "";

        private HubConnection? _hubConnection;
        private bool _isDisposed;
        private bool _isDisposing;
        private CancellationToken _stoppingToken;

        public AgentService(
            IDeploymentPackageAgent deploymentPackageAgent,
            ILogger logger,
            IMediator mediator,
            IHostApplicationLifetime lifetime,
            EnvironmentVariables environmentVariables,
            LoggingLevelSwitch loggingLevelSwitch,
            IKeyValueConfiguration keyValueConfiguration,
            AgentConfiguration? agentConfiguration = default)
        {
            _deploymentPackageAgent = deploymentPackageAgent;
            _logger = logger;
            _mediator = mediator;
            _lifetime = lifetime;
            _environmentVariables = environmentVariables;
            _loggingLevelSwitch = loggingLevelSwitch;
            _keyValueConfiguration = keyValueConfiguration;
            _agentConfiguration = agentConfiguration;
        }

        public async ValueTask DisposeAsync()
        {
            if (_isDisposed || _isDisposing)
            {
                return;
            }

            _isDisposing = true;

            if (_hubConnection is { })
            {
                _hubConnection.Closed -= HubConnectionOnClosed;

                await _hubConnection.StopAsync(_stoppingToken);

                _logger.Debug("Stopped SignalR in Agent {AgentId}", _agentId);

                await _hubConnection.DisposeAsync();
            }

            _subscriptions.ForEach(disposable => disposable.Dispose());
            _subscriptions.Clear();

            _isDisposing = false;
            _hubConnection = null;
            _isDisposed = true;
        }

        private async Task<bool> Connect()
        {
            if (_hubConnection is null || _agentConfiguration is null)
            {
                return false;
            }

            bool connected = false;
            try
            {
                _logger.Debug("Connecting to server via SignalR {Url}", _connectionUrl);
                await _hubConnection.StartAsync(_stoppingToken);
                await _hubConnection.SendAsync(AgentConstants.SignalRAgentHubAgentConnect, _stoppingToken);
                connected = true;
                _logger.Debug("Connected to server");
            }
            catch (Exception ex) when (!ex.IsFatal())
            {
                _logger.Warning(ex, "Could not connect to server {Url} from agent {Agent}", _connectionUrl, _agentId);

                if (_agentConfiguration.StartupDelay >= TimeSpan.FromMilliseconds(20))
                {
                    await Task.Delay(_agentConfiguration.StartupDelay!.Value, _stoppingToken);
                }
            }

            return connected;
        }

        private void CreateSignalRConnection(string connectionUrl)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(connectionUrl, options => options.AccessTokenProvider = GetAccessToken)
                .Build();

            _hubConnection.Closed += HubConnectionOnClosed;

            _subscriptions.Add(_hubConnection.On<string, string>(AgentConstants.SignalRServerToAgentDeployCommand,
                ExecuteDeploymentTask));
            _subscriptions.Add(_hubConnection.On<string>(AgentConstants.SignalRServerToAgentPingCommand, Ping));
            _subscriptions.Add(_hubConnection.On(AgentConstants.ServerShuttingDown, ShutDown));
            _subscriptions.Add(_hubConnection.On(AgentConstants.SignalRServerToAgentGetConfigCommand, SendConfig));
            _subscriptions.Add(_hubConnection.On<LogEventLevel>(AgentConstants.SignalRServerToAgentSetLogLevelCommand,
                SetLogLevel));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _stoppingToken = stoppingToken;

            if (_agentConfiguration is null)
            {
                _logger.Fatal("Agent configuration is missing");
                return;
            }

            _logger.Debug("Starting Agent service {Service}", nameof(AgentService));

            await Task.Yield();

            _agentId = _agentConfiguration?.AgentId();

            if (_agentId is null)
            {
                _logger.Error("Could not find agent id, token length is {TokenLength}",
                    _agentConfiguration?.AccessToken.Length.ToString(CultureInfo.InvariantCulture) ?? "N/A");
                return;
            }

            _connectionUrl = $"{_agentConfiguration!.ServerBaseUri}{AgentConstants.HubRoute}";

            CreateSignalRConnection(_connectionUrl);

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

                bool connected = false;

                while (!connected && _hubConnection is { } && !stoppingToken.IsCancellationRequested)
                {
                    connected = await Connect();
                }
            }
            catch (Exception ex) when (!ex.IsFatal())
            {
                _logger.Error(ex, "Could not connect to server {Url} from agent {Agent}", _connectionUrl, _agentId);
            }

            await SendConfig();

            _logger.Debug("Agent background service waiting for cancellation");
            await stoppingToken;
            _logger.Debug("Cancellation requested in Agent app");
            _logger.Debug("Stopping SignalR in Agent");
        }

        private async Task ExecuteDeploymentTask(string deploymentTaskId, string deploymentTargetId)
        {
            if (string.IsNullOrWhiteSpace(deploymentTaskId))
            {
                return;
            }

            var id = new DeploymentTargetId(deploymentTargetId);

            using CancellationTokenSource cancellationTokenSource =
                new(TimeSpan.FromMinutes(10));

            using var source =
                CancellationTokenSource.CreateLinkedTokenSource(_stoppingToken, cancellationTokenSource.Token);

            DeploymentTaskAgentResult deploymentTaskAgentResult;

            try
            {
                var exitCode =
                    await _deploymentPackageAgent.RunAsync(deploymentTaskId, id, cancellationTokenSource.Token);

                deploymentTaskAgentResult =
                    new DeploymentTaskAgentResult(deploymentTaskId, id, exitCode.IsSuccess);
            }
            catch (Exception ex) when (ex is OperationCanceledException or TaskCanceledException or TimeoutException)
            {
                _logger.Error("Build agent {AgentId} timed out for deployment task {DeploymentTaskId}",
                    _agentConfiguration.AgentId(), deploymentTaskId);
                deploymentTaskAgentResult =
                    new DeploymentTaskAgentResult(deploymentTaskId, id, false);
            }
            catch (Exception ex) when (!ex.IsFatal())
            {
                deploymentTaskAgentResult =
                    new DeploymentTaskAgentResult(deploymentTaskId, id, false);
            }

            await _mediator.Send(deploymentTaskAgentResult, _stoppingToken);
        }

        private Task<string> GetAccessToken() => Task.FromResult(_agentConfiguration!.AccessToken);

        private async Task HubConnectionOnClosed(Exception arg)
        {
            if (_stoppingToken.IsCancellationRequested)
            {
                return;
            }

            if (_hubConnection is { })
            {
                await Task.Delay(new Random().Next(0, 5) * 1000, _stoppingToken);
                await Connect();
            }
        }

        private Task Ping(string arg)
        {
            if (_logger.IsEnabled(LogEventLevel.Verbose))
            {
                _logger.Verbose("Received ping from server");
            }

            return Task.CompletedTask;
        }

        private async Task SendConfig()
        {
            string json = JsonConvert.SerializeObject(new AgentConfigurationView
            {
                EnvironmentVariables =
                    _environmentVariables.Variables.ToDictionary(pair => pair.Key, pair => pair.Value),
                ConfigurationItems = _keyValueConfiguration.AllWithMultipleValues.ToArray(),
                CurrentLogLevel = _loggingLevelSwitch.MinimumLevel
            });

            await _hubConnection.SendAsync(AgentConstants.SignalRAgentHubAgentConfig, json, _stoppingToken);
        }

        private Task SetLogLevel(LogEventLevel level)
        {
            _loggingLevelSwitch.MinimumLevel = level;
            return Task.CompletedTask;
        }

        private Task ShutDown()
        {
            if (_stoppingToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            _lifetime.StopApplication();

            return Task.CompletedTask;
        }
    }
}