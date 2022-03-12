using System;
using Arbor.AppModel.Messaging;
using JetBrains.Annotations;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Deployment
{
    public class DeploymentTargetLogged : IEvent
    {
        public DeploymentTargetLogged(DeploymentTargetId deploymentTargetId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(message));
            }

            DeploymentTargetId = deploymentTargetId;
            Message = message;
        }

        public DeploymentTargetId DeploymentTargetId { get; }

        public string Message { get; }
    }
}