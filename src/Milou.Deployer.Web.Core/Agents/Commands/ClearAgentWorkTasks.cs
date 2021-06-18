using Arbor.App.Extensions.Messaging;
using Arbor.Hypermedia;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Milou.Deployer.Web.Agent;
using NuGet.Protocol.Plugins;

namespace Milou.Deployer.Web.Core.Agents.Commands
{
    [UsedImplicitly]
    [Route(RouteTemplate, Name = RouteName)]
    [Arbor.Hypermedia.HttpDelete]
    public sealed record ClearAgentWorkTasks(AgentId AgentId) : ICommand<ClearAgentWorkTasksResult>, IMetadata
    {
        public const string RouteName = nameof(ClearAgentWorkTasks) + "Route"; // CodeGen
        public const string RouteTemplate = "/agent/{" + RouteParameterName + "}/workTasks"; // CodeGen

        public const string RouteParameterName = "agentId";


        public EntityMetadata CreateMetadata() => new Metadata(AgentId);

        public static EntityMetadata GetMetadata(AgentId agentId) => new Metadata(agentId);

        public record Metadata : EntityMetadata
        {
            public Metadata([NotNull] AgentId identifiable) : base(identifiable,
                ClearAgentWorkTasks.RouteName,
                ClearAgentWorkTasks.RouteParameterName,
                CustomHttpMethod.Delete)
            {
            }
        }
    }
}