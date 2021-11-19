using Arbor.AppModel.ExtensionMethods;
using Arbor.AppModel.Messaging;

namespace Milou.Deployer.Web.Core.Deployment.Messages
{
    public class CreateProject : ICommand<CreateProjectResult>
    {
        public CreateProject(string id, string organizationId)
        {
            Id = id;
            OrganizationId = organizationId;
        }

        public string Id { get; }

        public string OrganizationId { get; }

        public bool IsValid => Id.HasSomeString() && OrganizationId.HasSomeString();
    }
}