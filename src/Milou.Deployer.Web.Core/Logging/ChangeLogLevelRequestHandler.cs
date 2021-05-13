using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Milou.Deployer.Web.Agent;
using Serilog;

namespace Milou.Deployer.Web.Core.Logging
{
    [UsedImplicitly]
    public class ChangeLogLevelRequestHandler : IRequestHandler<ChangeLogLevelRequest>
    {
        private readonly LogLevelState _levelState;
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public ChangeLogLevelRequestHandler(LogLevelState levelState, ILogger logger, IMediator mediator)
        {
            _levelState = levelState;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<Unit> Handle([NotNull] ChangeLogLevelRequest? request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (LogEventLevelParser.TryParse(request.ChangeLogLevel.NewLevel, out var newLevel) && TimeSpan.TryParse(request.ChangeLogLevel.TimeSpan, out var timeSpan))
            {
                _levelState.SetLevel(newLevel, timeSpan);
               await _mediator.Publish(new LogLevelChanged(newLevel), cancellationToken);
            }
            else
            {
                _logger.Warning("Invalid log level request {Request}", request);
            }

            return Unit.Value;
        }
    }
}