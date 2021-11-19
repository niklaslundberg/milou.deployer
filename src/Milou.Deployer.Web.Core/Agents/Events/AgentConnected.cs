using Arbor.AppModel.Messaging;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Agents.Events
{
    public class AgentConnected : IEvent
    {
        public AgentConnected(AgentId agentId, string connectionId)
        {
            ConnectionId = connectionId;
            AgentId = agentId;
        }

        public string ConnectionId { get; }

        public AgentId AgentId { get; }
    }
}