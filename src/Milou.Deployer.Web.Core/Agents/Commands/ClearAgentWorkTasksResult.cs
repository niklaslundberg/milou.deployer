using Arbor.App.Extensions.Messaging;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Agents.Commands
{
    public class ClearAgentWorkTasksResult : ICommandResult
    {
        public ClearAgentWorkTasksResult(AgentId agentId) => AgentId = agentId;

        public AgentId AgentId { get; }
    }
}