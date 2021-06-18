using System.Collections.Generic;
using Arbor.Hypermedia;

namespace Milou.Deployer.Web.Core.Agents.Queries
{
    public record AgentMetadata : EntityMetadata
    {
        public AgentMetadata(Agent.AgentView agent, IEnumerable<EntityMetadata> supportedActions) : base(agent,
            GetAgentRequest.RouteName,
            GetAgentRequest.RouteParameterName,
            CustomHttpMethod.Get,
            supportedActions)
        {
        }
    }
}