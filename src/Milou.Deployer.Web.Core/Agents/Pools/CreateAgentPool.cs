using Arbor.AppModel.Messaging;

namespace Milou.Deployer.Web.Core.Agents.Pools
{
    public sealed record CreateAgentPool(AgentPoolId AgentPoolId, AgentPoolName Name) : ICommand<CreateAgentPoolResult>;
}