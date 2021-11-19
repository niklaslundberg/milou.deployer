using Arbor.AppModel.Messaging;

namespace Milou.Deployer.Web.Core.Agents.Pools
{
    public record GetAgentPoolsQuery : IQuery<AgentPoolListResult>;
}