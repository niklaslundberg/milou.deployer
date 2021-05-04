using Arbor.App.Extensions.Messaging;

namespace Milou.Deployer.Web.Core.Deployment.WorkTasks
{
    public record DeploymentTaskNotCreated(DeploymentTask DeploymentTask, string? Message) : IEvent;
}