using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using Arbor.AppModel.ExtensionMethods;
using JetBrains.Annotations;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.IisHost.Areas.Deployment.Signaling
{
    public class LogSubscribers
    {
        public ConcurrentDictionary<DeploymentTargetId, HashSet<string>> TargetMapping { get; } = new();

        public ImmutableHashSet<string> TryGetTargetSubscribers([NotNull] DeploymentTargetId deploymentTargetId)
        {
            bool tryGetTargetSubscribers = TargetMapping.TryGetValue(deploymentTargetId, out var subscribers);

            if (!tryGetTargetSubscribers)
            {
                return ImmutableHashSet<string>.Empty;
            }

            return subscribers.SafeToImmutableArray().ToImmutableHashSet();
        }
    }
}