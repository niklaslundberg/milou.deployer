using System;
using Arbor.App.Extensions;
using Arbor.ModelBinding.Primitives;

namespace Milou.Deployer.Web.Agent
{
    [StringValueType(StringComparison.OrdinalIgnoreCase)]
    public partial class DeploymentTargetId
    {
        public static readonly DeploymentTargetId Invalid = new(Constants.NotAvailable);

        public string TargetId => Value;

        public static bool TryParse(string? value, out DeploymentTargetId? deploymentTargetId)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                deploymentTargetId = default;

                return false;
            }

            deploymentTargetId = new DeploymentTargetId(value);

            return true;
        }
    }
}