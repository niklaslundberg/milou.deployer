using System;
using System.Collections.Immutable;
using Arbor.AppModel.ExtensionMethods;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NuGet.Versioning;

namespace Milou.Deployer.Core.Deployment
{
    public static class DeploymentExecutionDefinitionParser
    {
        public static ImmutableArray<DeploymentExecutionDefinitionV1> Deserialize([NotNull] string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(data));
            }

            try
            {
                var deploymentExecutionDefinitions =
                    JsonConvert.DeserializeObject<DeploymentExecutionDefinitions>(data);

                if (!string.IsNullOrWhiteSpace(deploymentExecutionDefinitions?.Version) && (!SemanticVersion.TryParse(deploymentExecutionDefinitions?.Version, out var semanticVersion) ||
                    semanticVersion.Major != 1))
                {
                    throw new InvalidOperationException(
                        "Only version 1 of deployment execution definitions are supported");
                }

                if (deploymentExecutionDefinitions?.Definitions is null)
                {
                    return ImmutableArray<DeploymentExecutionDefinitionV1>.Empty;
                }

                return deploymentExecutionDefinitions.Definitions.SafeToImmutableArray();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Could not parse deployment execution definitions from data '{data}'",
                    ex);
            }
        }
    }
}