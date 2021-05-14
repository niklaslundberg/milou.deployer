using System.Collections.Immutable;
using Arbor.App.Extensions.Messaging;

namespace Milou.Deployer.Web.Core.Agents.Queries
{
    public class AgentsQueryResult : IQueryResult
    {
        public AgentsQueryResult(ImmutableArray<AgentInfo> agents) => Agents = agents;

        public ImmutableArray<AgentInfo> Agents { get; }
    }
}