using Arbor.AppModel.Messaging;

namespace Milou.Deployer.Web.Agent
{
    public class AgentDisconnected : IEvent
    {
        public AgentDisconnected(AgentId agentId) => AgentId = agentId;

        public AgentId AgentId { get; }
    }
}