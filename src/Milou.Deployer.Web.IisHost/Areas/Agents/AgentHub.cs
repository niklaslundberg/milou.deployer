using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Milou.Deployer.Web.Agent;
using Milou.Deployer.Web.Core.Agents;
using Milou.Deployer.Web.Core.Agents.Events;
using Milou.Deployer.Web.Core.Agents.Queries;
using Milou.Deployer.Web.Core.Security;
using Newtonsoft.Json;
using Serilog;

namespace Milou.Deployer.Web.IisHost.Areas.Agents
{
    [Authorize(Policy = AuthorizationPolicies.Agent)]
    [UsedImplicitly]
    public class AgentHub : Hub
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public AgentHub(IMediator mediator, ILogger logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
        }

        [PublicAPI]
        public async Task AgentConfig(string agentConfig)
        {
            if (!AgentId.TryParse(Context.UserIdentifier, out var agentId))
            {
                _logger.Warning("The connected agent has no agent id");

                return;
            }

            var agentConfigView = JsonConvert.DeserializeObject<AgentConfigurationView>(agentConfig);

            if (agentConfigView is null)
            {
                return;
            }

            await _mediator.Publish(new AgentConfigResponse(agentId, agentConfigView));
        }

        [PublicAPI]
        public async Task AgentConnect()
        {
            if (!AgentId.TryParse(Context.UserIdentifier, out var agentId))
            {
                _logger.Warning("The connected agent has no agent id");

                return;
            }

            var agentInfo = await _mediator.Send(new GetAgentRequest(agentId));

            if (agentInfo is null)
            {
                await _mediator.Publish(new UnknownAgentConnected(agentId, Context.ConnectionId));
                _logger.Warning("Unknown agent {AgentId} connected", agentId);

                return;
            }

            await _mediator.Publish(new AgentConnected(agentId, Context.ConnectionId));
        }

        public override Task OnConnectedAsync()
        {
            _logger.Verbose("SignalR Agent client connected, identity {Identity}", Context.User?.Identity?.Name);

            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception) => await _mediator.Publish(
            new AgentDisconnected(new AgentId(Context.UserIdentifier ??
                                              throw new InvalidOperationException(
                                                  "Missing user identifier on context"))));
    }
}