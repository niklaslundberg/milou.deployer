using Arbor.AppModel.Messaging;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Agents
{
    public record AgentConfigResponse(AgentId AgentId, AgentConfigurationView agentConfigurationView) : IEvent;
}