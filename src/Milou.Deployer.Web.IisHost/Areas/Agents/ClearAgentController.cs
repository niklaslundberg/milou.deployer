using System.Threading.Tasks;
using Arbor.Hypermedia;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Milou.Deployer.Web.Agent;
using Milou.Deployer.Web.Core.Agents.Commands;
using Milou.Deployer.Web.IisHost.Controllers;

namespace Milou.Deployer.Web.IisHost.Areas.Agents
{
    public class ClearAgentController : BaseApiController
    {
        //[ValidateAntiForgeryToken]
        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Route(Core.Agents.Commands.ClearAgentWorkTasks.RouteTemplate,
            Name = Core.Agents.Commands.ClearAgentWorkTasks.RouteName)]
        public async Task<IActionResult> ClearAgentWorkTasks([FromRoute] AgentId agentId,
            [FromServices] IMediator mediator,
            [FromServices] HyperMediaResult hyperMediaResult)
        {
            var result = await mediator.Send(new ClearAgentWorkTasks(agentId));

            return await hyperMediaResult.ToHyperMediaResult(this, result.Result);
        }
    }
}