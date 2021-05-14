using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Milou.Deployer.Web.Agent;
using Milou.Deployer.Web.Core.Agents;
using Milou.Deployer.Web.Core.Agents.Events;

namespace Milou.Deployer.Web.IisHost.Areas.Agents
{
    [UsedImplicitly]
    public class AgentStatusHandler : INotificationHandler<AgentConnected>, INotificationHandler<AgentDisconnected>,
        INotificationHandler<AgentConfigResponse>
    {
        private readonly AgentsData _agents;

        public AgentStatusHandler(AgentsData agents) => _agents = agents;

        public Task Handle(AgentConfigResponse notification, CancellationToken cancellationToken)
        {
            _agents.SetConfig(notification);

            return Task.CompletedTask;
        }

        public Task Handle(AgentConnected agentConnected, CancellationToken cancellationToken)
        {
            _agents.AgentConnected(agentConnected);

            return Task.CompletedTask;
        }

        public Task Handle(AgentDisconnected agentDisconnected, CancellationToken cancellationToken)
        {
            _agents.AgentDisconnected(agentDisconnected);

            return Task.CompletedTask;
        }
    }
}