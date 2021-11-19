using Arbor.AppModel.Messaging;

namespace Milou.Deployer.Web.Core.Agents.Commands
{
    public class AgentStatusResult : ICommandResult<Queries.Agent>
    {
        public Queries.Agent Result { get; }

        public AgentStatusResult(Queries.Agent agent)
        {
            Result = agent;
        }
    }
}