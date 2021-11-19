using Arbor.AppModel.Messaging;
using Arbor.Hypermedia;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Agents.Commands
{
    [UsedImplicitly]
    [Route(RouteTemplate, Name = RouteName)]
    [Arbor.Hypermedia.HttpDelete]
    public sealed record DisableAgent(AgentId AgentId) : ICommand<AgentStatusResult>, IMetadata
    {
        public const string RouteName = nameof(DisableAgent) + "Route"; // CodeGen
        public const string RouteTemplate = "/agent/{" + RouteParameterName + "}/enabled"; // CodeGen
        public const string RouteParameterName = "agentId";


        public EntityMetadata CreateMetadata() => new Metadata(AgentId);

        // CodeGen
        public static EntityMetadata GetMetadata(AgentId agentId) => new Metadata(agentId);

        // CodeGen
        public record Metadata : EntityMetadata
        {
            public Metadata([NotNull] AgentId identifiable) : base(identifiable,
                DisableAgent.RouteName,
                DisableAgent.RouteParameterName,
                CustomHttpMethod.Delete)
            {
            }
        }
    }
}