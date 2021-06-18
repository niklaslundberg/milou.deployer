using System.Threading.Tasks;
using Arbor.Hypermedia;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Milou.Deployer.Web.Agent;
using Milou.Deployer.Web.Core.Agents.Queries;
using Milou.Deployer.Web.IisHost.Controllers;

namespace Milou.Deployer.Web.IisHost.Areas.Agents
{
    public class GetAgentController : BaseApiController
    {
        public const string Route = "~/agents/{agentId}";
        public const string RouteName = nameof(GetAgentController) + nameof(Route);

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route(GetAgentRequest.RouteTemplate, Name = GetAgentRequest.RouteName)]
        public async Task<IActionResult> GetAgent(
            [FromRoute] AgentId agentId,
            [FromServices] IMediator mediator,
            [FromServices] HyperMediaResult hyperMediaResult)
        {
            var getAgentRequest = new GetAgentRequest(agentId);

            var queryResult = await mediator.Send(getAgentRequest);

            return await hyperMediaResult.ToHyperMediaResult(this, queryResult.Result);
        }
    }
}