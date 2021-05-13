using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Milou.Deployer.Web.Core.Deployment;

namespace Milou.Deployer.Web.IisHost.Areas.Deployment.Signaling
{
    [UsedImplicitly]
    public class DeploymentHubLogHandler : INotificationHandler<DeploymentTargetLogged>
    {
        private readonly IHubContext<TargetHub> _hubContext;
        private readonly LogSubscribers _logSubscribers;

        public DeploymentHubLogHandler([NotNull] IHubContext<TargetHub> hubContext, LogSubscribers logSubscribers)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _logSubscribers = logSubscribers;
        }

        public async Task Handle(DeploymentTargetLogged notification, CancellationToken cancellationToken)
        {
            ImmutableHashSet<string> tryGetTargetSubscribers =
                _logSubscribers.TryGetTargetSubscribers(notification.DeploymentTargetId);

            if (tryGetTargetSubscribers.Count == 0)
            {
                return;
            }

            string[] clients = tryGetTargetSubscribers.ToArray();
            IClientProxy clientProxy = _hubContext.Clients.Clients(clients);

            await clientProxy.SendAsync(TargetHub.MessageMethod, notification.Message, cancellationToken);
        }
    }
}