using System.Collections.Immutable;
using Arbor.AppModel.ExtensionMethods;
using Arbor.AppModel.Messaging;
using Arbor.KVConfiguration.Core;

namespace Milou.Deployer.Web.Core.Deployment.Messages
{
    public class CreateOrganizationResult : ICommandResult
    {
        public CreateOrganizationResult(params ValidationError[] validationErrors) =>
            ValidationErrors = validationErrors.SafeToImmutableArray();

        public ImmutableArray<ValidationError> ValidationErrors { get; }
    }
}