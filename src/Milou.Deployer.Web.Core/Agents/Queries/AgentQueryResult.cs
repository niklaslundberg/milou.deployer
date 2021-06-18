using Milou.Deployer.Web.Core.Agents.Commands;

namespace Milou.Deployer.Web.Core.Agents.Queries
{
    public record AgentQueryResult(Agent Result) : IQueryResult<Agent>;
}