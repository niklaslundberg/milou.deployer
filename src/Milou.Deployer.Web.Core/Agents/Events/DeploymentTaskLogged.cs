using Arbor.AppModel.Messaging;
using Milou.Deployer.Web.Agent;
using Serilog.Events;

namespace Milou.Deployer.Web.Core.Agents.Events
{
    public class DeploymentTaskLogged : IEvent
    {
        public DeploymentTaskLogged(string deploymentTaskId,
            DeploymentTargetId deploymentTargetId,
            string message,
            LogEventLevel logEventLevel)
        {
            DeploymentTaskId = deploymentTaskId;
            DeploymentTargetId = deploymentTargetId;
            Message = message;
            LogEventLevel = logEventLevel;
        }

        public DeploymentTargetId DeploymentTargetId { get; }

        public string Message { get; }

        public LogEventLevel LogEventLevel { get; }

        public string DeploymentTaskId { get; }
    }
}