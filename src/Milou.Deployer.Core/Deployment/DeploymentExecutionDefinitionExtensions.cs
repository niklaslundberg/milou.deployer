using System;
using JetBrains.Annotations;
using Milou.Deployer.Core.Deployment.Configuration;

namespace Milou.Deployer.Core.Deployment
{
    public static class DeploymentExecutionDefinitionExtensions
    {
        public static bool AppDataSkipDirectiveEnabled(
            this DeploymentExecutionDefinitionV1 deploymentExecutionDefinition,
            bool defaultValue = false)
        {
            if (deploymentExecutionDefinition is null)
            {
                throw new ArgumentNullException(nameof(deploymentExecutionDefinition));
            }

            return GetBoolValue(deploymentExecutionDefinition,
                defaultValue,
                WebDeployRules.AppDataSkipDirectiveEnabled);
        }

        public static bool ApplicationInsightsProfiler2SkipDirectiveEnabled(
            this DeploymentExecutionDefinitionV1 deploymentExecutionDefinition,
            bool defaultValue = true)
        {
            if (deploymentExecutionDefinition is null)
            {
                throw new ArgumentNullException(nameof(deploymentExecutionDefinition));
            }

            return GetBoolValue(deploymentExecutionDefinition,
                defaultValue,
                WebDeployRules.ApplicationInsightsProfiler2SkipDirectiveEnabled);
        }

        public static bool AppOfflineEnabled(this DeploymentExecutionDefinitionV1 deploymentExecutionDefinition,
            bool defaultValue = true)
        {
            if (deploymentExecutionDefinition is null)
            {
                throw new ArgumentNullException(nameof(deploymentExecutionDefinition));
            }

            return GetBoolValue(deploymentExecutionDefinition, defaultValue, WebDeployRules.AppOfflineEnabled);
        }

        public static bool DoNotDeleteEnabled(
            this DeploymentExecutionDefinitionV1 deploymentExecutionDefinition,
            bool defaultValue = true)
        {
            if (deploymentExecutionDefinition is null)
            {
                throw new ArgumentNullException(nameof(deploymentExecutionDefinition));
            }

            return GetBoolValue(deploymentExecutionDefinition, defaultValue, WebDeployRules.DoNotDeleteEnabled);
        }

        public static bool UseChecksumEnabled(
            this DeploymentExecutionDefinitionV1 deploymentExecutionDefinition,
            bool defaultValue = false)
        {
            if (deploymentExecutionDefinition is null)
            {
                throw new ArgumentNullException(nameof(deploymentExecutionDefinition));
            }

            return GetBoolValue(deploymentExecutionDefinition, defaultValue, WebDeployRules.UseChecksumEnabled);
        }

        public static bool WhatIfEnabled(this DeploymentExecutionDefinitionV1 deploymentExecutionDefinition,
            bool defaultValue = false)
        {
            if (deploymentExecutionDefinition is null)
            {
                throw new ArgumentNullException(nameof(deploymentExecutionDefinition));
            }

            return GetBoolValue(deploymentExecutionDefinition, defaultValue, WebDeployRules.WhatIfEnabled);
        }

        private static bool GetBoolValue(DeploymentExecutionDefinitionV1 deploymentExecutionDefinition,
            bool defaultValue,
            string configurationKey)
        {
            deploymentExecutionDefinition.Parameters.TryGetValue(configurationKey, out var values);

            if (values.Count == 1 && bool.TryParse(values[0], out bool flag))
            {
                return flag;
            }

            return defaultValue;
        }
    }
}