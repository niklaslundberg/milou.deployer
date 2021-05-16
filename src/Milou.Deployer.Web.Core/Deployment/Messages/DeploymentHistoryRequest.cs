using Arbor.App.Extensions.Messaging;

namespace Milou.Deployer.Web.Core.Deployment.Messages
{
    public class DeploymentHistoryRequest : IQuery<DeploymentHistoryResponse>
    {
        public DeploymentHistoryRequest(string deploymentTargetId, int page, int pageSize)
        {
            DeploymentTargetId = deploymentTargetId;
            Page = page;
            PageSize = pageSize;
        }

        public string DeploymentTargetId { get; }

        public int Page { get; }

        public int PageSize { get; }
    }
}