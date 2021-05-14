using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Milou.Deployer.Web.Core.Deployment.WorkTasks;

namespace Milou.Deployer.Web.IisHost.Areas.Deployment.Signaling
{
    [UsedImplicitly]
    public class DeploymentTaskNotCreatedHandler : INotificationHandler<DeploymentTaskNotCreated>
    {
        private readonly IHubContext<TargetHub> _hubContext;
        private readonly LogSubscribers _logSubscribers;

        public DeploymentTaskNotCreatedHandler([NotNull] IHubContext<TargetHub> hubContext,
            LogSubscribers logSubscribers)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _logSubscribers = logSubscribers;
        }

        public async Task Handle(DeploymentTaskNotCreated notification, CancellationToken cancellationToken)
        {
            ImmutableHashSet<string> tryGetTargetSubscribers =
                _logSubscribers.TryGetTargetSubscribers(notification.DeploymentTask.DeploymentTargetId);

            if (tryGetTargetSubscribers.Count == 0)
            {
                return;
            }

            string[] clients = tryGetTargetSubscribers.ToArray();
            IClientProxy clientProxy = _hubContext.Clients.Clients(clients);

            await clientProxy.SendAsync(TargetHub.MessageMethod,
                notification.Message ?? "Unknown error",
                cancellationToken);
        }
    }
}