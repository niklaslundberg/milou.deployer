using System.Collections.Immutable;
using Arbor.AppModel.Messaging;

namespace Milou.Deployer.Web.Core.Agents.Pools
{
    public class AgentPoolListResult : IQueryResult
    {
        public AgentPoolListResult(ImmutableArray<AgentPoolInfo> agentPools) => AgentPools = agentPools;

        public ImmutableArray<AgentPoolInfo> AgentPools { get; }
    }
}