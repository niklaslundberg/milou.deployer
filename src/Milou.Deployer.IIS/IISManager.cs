using System;
using System.Collections.Generic;
using System.Linq;
using Arbor.AppModel.ExtensionMethods;
using JetBrains.Annotations;
using Microsoft.Web.Administration;
using Milou.Deployer.Core.Deployment;
using Milou.Deployer.Core.Deployment.Configuration;
using Serilog;
using Serilog.Events;

namespace Milou.Deployer.IIS
{
    [UsedImplicitly]
    public sealed class IisManager : IIisManager
    {
        private readonly DeployerConfiguration _configuration;
        private readonly DeploymentExecutionDefinitionV1 _deploymentExecutionDefinitionV1;
        private readonly ILogger _logger;
        private ObjectState _previousSiteState;
        private ServerManager? _serverManager;
        private Site? _site;
        private readonly Dictionary<ApplicationPool, ObjectState> _appPools = new (2);

        private IisManager(ServerManager serverManager,
            DeployerConfiguration configuration,
            ILogger logger,
            DeploymentExecutionDefinitionV1 deploymentExecutionDefinition)
        {
            _serverManager = serverManager;
            _configuration = configuration;
            _logger = logger;
            _deploymentExecutionDefinitionV1 = deploymentExecutionDefinition;
        }

        public void Dispose()
        {
            bool restored = RestoreState();

            if (!_logger.IsEnabled(LogEventLevel.Debug))
            {
                return;
            }

            if (restored)
            {
                _logger.Debug(
                    "Restored iis site state to {State} for site {SiteName} defined in deployment execution definition {DeploymentExecutionDefinition}",
                    _site!.State,
                    _deploymentExecutionDefinitionV1.IisSiteName,
                    _deploymentExecutionDefinitionV1);
            }
            else
            {
                _logger.Debug(
                    "Failed to restore iis site state for site {SiteName} defined in deployment execution definition {DeploymentExecutionDefinition}",
                    _deploymentExecutionDefinitionV1.IisSiteName,
                    _deploymentExecutionDefinitionV1);
            }
        }

        public bool StopSiteIfApplicable()
        {
            if (!UserHelper.IsAdministrator())
            {
                _logger.Warning("Current user does not have administrative privileges, cannot start/stop site");

                return false;
            }

            _site = null!;
            _previousSiteState = ObjectState.Unknown;

            if (_serverManager is null)
            {
                _logger.Error(
                    "There is no ServerManager instance when trying to stop IIS site defined in {DeploymentExecutionDefinition}",
                    _deploymentExecutionDefinitionV1);

                return false;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(_deploymentExecutionDefinitionV1.IisSiteName))
                {
                    if (_logger.IsEnabled(LogEventLevel.Debug))
                    {
                        _logger.Debug(
                            "The deployment execution definition {DeploymentExecutionDefinition} has no site named defined",
                            _deploymentExecutionDefinitionV1);
                    }

                    return false;
                }

                if (!_configuration.StopStartIisWebSiteEnabled)
                {
                    _logger.Warning("The deployer configuration has {Property} set to false",
                        nameof(_configuration.StopStartIisWebSiteEnabled));

                    return false;
                }

                _site = _serverManager.Sites[_deploymentExecutionDefinitionV1.IisSiteName];

                if (_site is null)
                {
                    _logger.Error(
                        "Could not find IIS site {SiteName} defined in deployment execution definition {DeploymentExecutionDefinition}",
                        _deploymentExecutionDefinitionV1.IisSiteName,
                        _deploymentExecutionDefinitionV1);

                    return false;
                }

                _previousSiteState = _site.State;

                if (_previousSiteState == ObjectState.Starting || _previousSiteState == ObjectState.Started)
                {
                    _logger.Information("Stopping IIS site '{IISSiteName}'", _site.Name);

                    var objectState = _site.Stop();

                    string[] appPoolNames = _site.Applications.Select(app => app.ApplicationPoolName).Distinct().ToArray();

                    if (_configuration.StopStartIisWebSiteAppPoolEnabled)
                    {
                        foreach (string? appPoolName in appPoolNames)
                        {
                            var appPool = _serverManager.ApplicationPools[appPoolName];

                            if (appPool.State is ObjectState.Started or ObjectState.Starting)
                            {
                                _appPools.Add(appPool, appPool.State);
                            }

                        }
                    }

                    if (objectState == ObjectState.Stopped)
                    {
                        _logger.Information("Stopped IIS site '{IISSiteName}'", _site.Name);
                    }

                    if (objectState == ObjectState.Stopping)
                    {
                        _logger.Information("Stopping IIS site '{IISSiteName}'", _site.Name);
                    }

                    foreach (var appPool in _appPools)
                    {
                        _logger.Information("Stopping application pool {ApplicationPool}", appPool.Key.Name);
                        appPool.Key.Stop();
                        _logger.Information("Stopped application pool {ApplicationPool}", appPool.Key.Name);
                    }
                }
            }
            catch (Exception ex) when (!ex.IsFatal())
            {
                _logger.Error(ex, "Error while trying to stop IIS site");

                return false;
            }

            return true;
        }

        public static IisManager? Create(DeployerConfiguration configuration,
            ILogger logger,
            DeploymentExecutionDefinitionV1 deploymentExecutionDefinition)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            if (deploymentExecutionDefinition is null)
            {
                throw new ArgumentNullException(nameof(deploymentExecutionDefinition));
            }

            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                logger.Warning("IIS operations are not supported on non-Windows platforms");
                return null;
            }

            return new IisManager(new ServerManager(), configuration, logger, deploymentExecutionDefinition);
        }

        private bool RestoreState()
        {
            try
            {
                if (!UserHelper.IsAdministrator())
                {
                    _logger.Warning("Current user does not have administrative privileges, cannot start/stop site");

                    return false;
                }

                if (_serverManager is { } && _appPools.Any())
                {
                    foreach (var (appPool, _) in _appPools)
                    {
                        _logger.Information("Starting application pool {ApplicationPool}", appPool.Name);
                        appPool.Start();
                        _logger.Information("Started application pool {ApplicationPool}", appPool.Name);
                    }
                }

                if (_serverManager is { } &&
                    _site is { } &&
                    _site.State != ObjectState.Starting &&
                    _site.State != ObjectState.Started &&
                    (_previousSiteState == ObjectState.Starting || _previousSiteState == ObjectState.Started))
                {
                    _logger.Information("Starting IIS site '{IISSiteName}'", _site.Name);

                    var objectState = _site.Start();

                    if (objectState == ObjectState.Started)
                    {
                        _logger.Information("Started IIS site '{IISSiteName}'", _site.Name);
                    }

                    if (objectState == ObjectState.Starting)
                    {
                        _logger.Information("Starting IIS site is in progress'{IISSiteName}'", _site.Name);
                    }
                }
            }
            catch (Exception ex) when (!ex.IsFatal())
            {
                _logger.Error(ex, "Could not restart site {IISSiteName}", _site!.Name);
            }
            finally
            {
                _serverManager?.Dispose();
                _serverManager = null!;
                _site = null!;
            }

            return true;
        }
    }
}