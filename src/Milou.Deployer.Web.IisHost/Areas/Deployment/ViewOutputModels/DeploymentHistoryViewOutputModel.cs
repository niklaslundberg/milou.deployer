using System.Collections.Generic;
using Milou.Deployer.Web.Core.Deployment.WorkTasks;

namespace Milou.Deployer.Web.IisHost.Areas.Deployment.ViewOutputModels
{
    public class DeploymentHistoryViewOutputModel
    {
        public DeploymentHistoryViewOutputModel(IReadOnlyCollection<DeploymentTaskInfo> deploymentTasks,
            int totalCount,
            int pages,
            int page,
            int pageSize)
        {
            DeploymentTasks = deploymentTasks;
            TotalCount = totalCount;
            Pages = pages;
            CurrentPage = page;
            PageSize = pageSize;
        }

        public int PageSize { get; }

        public int CurrentPage { get; }

        public IReadOnlyCollection<DeploymentTaskInfo> DeploymentTasks { get; }

        public int TotalCount { get; }

        public int Pages { get; }
    }
}