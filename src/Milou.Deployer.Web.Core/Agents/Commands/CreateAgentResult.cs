using Arbor.App.Extensions.Messaging;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Agents.Commands
{
    public class CreateAgentResult : ICommandResult
    {
        public CreateAgentResult(AgentId agentId, string accessToken)
        {
            AgentId = agentId;
            AccessToken = accessToken;
        }

        public AgentId AgentId { get; }
        public string AccessToken { get; }
    }
}