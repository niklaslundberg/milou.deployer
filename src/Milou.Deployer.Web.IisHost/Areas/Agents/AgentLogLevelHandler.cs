using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.IisHost.Areas.Agents
{
    public class AgentLogLevelHandler : INotificationHandler<LogLevelChanged>
    {
        private readonly IHubContext<AgentHub> _agentContext;

        public AgentLogLevelHandler(IHubContext<AgentHub> agentContext) => _agentContext = agentContext;

        public async Task Handle(LogLevelChanged notification, CancellationToken cancellationToken) =>
            await _agentContext.Clients.All.SendAsync(AgentConstants.SignalRServerToAgentSetLogLevelCommand, notification.NewLevel,
                cancellationToken);
    }
}