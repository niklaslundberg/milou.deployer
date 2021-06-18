using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Milou.Deployer.Web.Core.Agents.Queries;

namespace Milou.Deployer.Web.Core.Agents.Commands
{
    public class DisableAgentHandler : IRequestHandler<DisableAgent, AgentStatusResult>
    {
        private readonly IMediator _mediator;

        public DisableAgentHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<AgentStatusResult> Handle(DisableAgent request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAgentRequest(request.AgentId));

            return new AgentStatusResult(result.Result);
        }
    }
}