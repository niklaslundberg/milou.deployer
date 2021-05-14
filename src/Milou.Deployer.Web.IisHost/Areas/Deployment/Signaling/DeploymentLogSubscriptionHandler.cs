using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Milou.Deployer.Web.IisHost.Areas.Deployment.Messages;

namespace Milou.Deployer.Web.IisHost.Areas.Deployment.Signaling
{
    [UsedImplicitly]
    public class DeploymentLogSubscriptionHandler : IRequestHandler<SubscribeToDeploymentLog>,
        IRequestHandler<UnsubscribeToDeploymentLog>
    {
        private readonly LogSubscribers _logSubscribers;

        public DeploymentLogSubscriptionHandler(LogSubscribers logSubscribers) => _logSubscribers = logSubscribers;

        public Task<Unit> Handle(SubscribeToDeploymentLog request, CancellationToken cancellationToken)
        {
            if (_logSubscribers.TargetMapping.TryGetValue(request.DeploymentTargetId, out var subscribers))
            {
                subscribers.Add(request.ConnectionId);
            }
            else
            {
                _logSubscribers.TargetMapping.TryAdd(request.DeploymentTargetId,
                    new HashSet<string>(StringComparer.OrdinalIgnoreCase) {request.ConnectionId});
            }

            return Task.FromResult(Unit.Value);
        }

        public Task<Unit> Handle(UnsubscribeToDeploymentLog request, CancellationToken cancellationToken)
        {
            HashSet<string>[] hashSets = _logSubscribers.TargetMapping.Values.ToImmutableArray()
                                                        .Where(value => value.Contains(request.ConnectionId)).ToArray();

            foreach (HashSet<string> hashSet in hashSets)
            {
                hashSet.Remove(request.ConnectionId);
            }

            return Task.FromResult(Unit.Value);
        }
    }
}