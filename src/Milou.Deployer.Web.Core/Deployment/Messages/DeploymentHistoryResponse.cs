using System.Collections.Generic;
using Arbor.AppModel.Messaging;
using Milou.Deployer.Web.Core.Deployment.WorkTasks;

namespace Milou.Deployer.Web.Core.Deployment.Messages
{
    public class DeploymentHistoryResponse : IQueryResult
    {
        public DeploymentHistoryResponse(IReadOnlyCollection<DeploymentTaskInfo> deploymentTasks,
            int totalCount,
            int pages)
        {
            DeploymentTasks = deploymentTasks;
            TotalCount = totalCount;
            Pages = pages;
        }

        public IReadOnlyCollection<DeploymentTaskInfo> DeploymentTasks { get; }

        public int TotalCount { get; }

        public int Pages { get; }
    }
}