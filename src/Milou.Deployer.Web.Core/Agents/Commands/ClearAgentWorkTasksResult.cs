namespace Milou.Deployer.Web.Core.Agents.Commands
{
    public record ClearAgentWorkTasksResult(Queries.Agent Result) : ICommandResult<Queries.Agent>
    {
    }
}
