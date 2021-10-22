using System.Threading.Tasks;
using Arbor.Hypermedia;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Milou.Deployer.Web.Agent;
using Milou.Deployer.Web.Core.Agents.Commands;
using Milou.Deployer.Web.IisHost.Controllers;

namespace Milou.Deployer.Web.IisHost.Areas.Agents
{
    public class DisableAgentController : BaseApiController
    {
        //[ValidateAntiForgeryToken]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Route(Core.Agents.Commands.DisableAgent.RouteTemplate, Name = Core.Agents.Commands.DisableAgent.RouteName)]
        public async Task<ActionResult> DisableAgent([FromRoute] AgentId agentId,
            [FromServices] IMediator mediator)
        {
            var result = await mediator.Send(new DisableAgent(agentId));

            return Ok(result);
        }
    }
}