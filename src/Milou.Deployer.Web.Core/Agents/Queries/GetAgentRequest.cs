using Arbor.AppModel.Messaging;
using MediatR;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Agents.Queries
{
    public sealed record GetAgentRequest(AgentId AgentId) : IQuery<AgentQueryResult>
    {
        public const string RouteTemplate = "/agents/{" + RouteParameterName + "}/"; // CodeGen

        public const string RouteName = nameof(GetAgentRequest) + "Route";

        public const string RouteParameterName = "agentId";
    }
}