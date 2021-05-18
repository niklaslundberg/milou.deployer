using System;
using System.Collections.Generic;
using FluentAssertions;
using Milou.Deployer.Core.Deployment;
using Milou.Deployer.Core.Deployment.Configuration;
using Newtonsoft.Json;
using NuGet.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace Milou.Deployer.Tests.Integration
{
    public class WhenSerializingManifest
    {
        public WhenSerializingManifest(ITestOutputHelper output) => _output = output;

        private readonly ITestOutputHelper _output;

        [Fact]
        public void DefinitionCreatedWithPublicCtorShouldBeEqualToDeserializedDefinition()
        {
            var definition = new DeploymentExecutionDefinitionV1("aPackageId",
                @"C:\Temp",
                new SemanticVersion(1, 2, 3),
                "@C:\\Nuget.Config",
                "aNuGetSource",
                "aSiteName",
                true,
                false,
                "production",
                null,
                null,
                null,
                false,
                "C:\\Xdt.Config",
                PublishType.WebDeploy.Name,
                null,
                "C:\\NuGet.exe",
                "packageid:",
                true);

            DeploymentExecutionDefinitionV1[] deploymentExecutionDefinitions = {definition};

            string serialized = JsonConvert.SerializeObject(new {definitions = deploymentExecutionDefinitions},
                Formatting.Indented);

            _output.WriteLine(serialized);

            var deserializedObject = DeploymentExecutionDefinitionParser.Deserialize(serialized);

            Assert.Single(deserializedObject);

            string serializedDeserialized = JsonConvert.SerializeObject(
                new {definitions = deploymentExecutionDefinitions},
                Formatting.Indented);

            DeploymentExecutionDefinitionV1 deserializedDefinitionV1 = deserializedObject[0];

            Assert.Equal(serialized, serializedDeserialized);

            Assert.Equal(definition.PackageId, deserializedDefinitionV1.PackageId);
        }

        [Theory]
        [InlineData("\"2.0.0\"")]
        [InlineData("\"1\"")]
        public void InvalidOrUnsupportedVersionShouldThrowException(string version)
        {
            Action deserialize = () => DeploymentExecutionDefinitionParser.Deserialize($"{{\"version\":{version}}}");

            deserialize.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData("\"1.0.0\"")]
        [InlineData("\"\"")]
        [InlineData("null")]
        public void EmptyOrSupportedVersionShouldNotThrowException(string version)
        {
            Action deserialize = () => DeploymentExecutionDefinitionParser.Deserialize($"{{\"version\":{version}}}");

            deserialize.Should().NotThrow();
        }

        [Fact]
        public void NotPresentVersionShouldNotThrowException()
        {
            Action deserialize = () => DeploymentExecutionDefinitionParser.Deserialize("{}");

            deserialize.Should().NotThrow();
        }

        [Fact]
        public void ItShouldFind1DeploymentExecutionDefinition()
        {
            var parameters = new Dictionary<string, string[]>
            {
                [WebDeployRules.AppDataSkipDirectiveEnabled] = new[] {"false"},
                [WebDeployRules.AppOfflineEnabled] = new[] {"true"},
                [WebDeployRules.ApplicationInsightsProfiler2SkipDirectiveEnabled] = new[] {"true"},
                [WebDeployRules.DoNotDeleteEnabled] = new[] {"false"},
                [WebDeployRules.UseChecksumEnabled] = new[] {"true"},
                [WebDeployRules.WhatIfEnabled] = new[] {"false"}
            };

            DeploymentExecutionDefinitionV1[] deploymentExecutionDefinitions =
            {
                new("MySamplePackageId", @"C:\Sites\Sample", SemanticVersion.Parse("1.2.3"), excludedFilePatterns:
                    "*.user;*.cache", parameters: parameters)
            };

            string serialized = JsonConvert.SerializeObject(new {definitions = deploymentExecutionDefinitions},
                Formatting.Indented);

            _output.WriteLine(serialized);

            var deserializeObject = DeploymentExecutionDefinitionParser.Deserialize(serialized);

            Assert.Single(deserializeObject);
            Assert.Equal(2, deserializeObject[0].ExcludedFilePatterns.Length);
            Assert.Equal(6, deserializeObject[0].Parameters.Count);
            deserializeObject[0].Version.Should().Be("1.2.3");
            deserializeObject[0].PackageId.Should().Be("MySamplePackageId");
            deserializeObject[0].TargetDirectoryPath.Should().Be(@"C:\Sites\Sample");
        }
    }
}