using System;
using System.IO;
using Arbor.AppModel.Application;
using Arbor.AppModel.Configuration;

namespace Milou.Deployer.Web.Tests.Integration
{
    public sealed class ServerEnvironmentTestConfiguration : IConfigureEnvironment, IDisposable
    {
        public ServerEnvironmentTestConfiguration(PortPoolRental portPoolRental, DirectoryInfo tempDir)
        {
            Port = portPoolRental;
            TempDir = tempDir;
        }

        public PortPoolRental Port { get; }

        public DirectoryInfo TempDir { get; }

        public void Configure(EnvironmentConfiguration environmentConfiguration)
        {
            environmentConfiguration.HttpPort = Port.Port;
            environmentConfiguration.HttpEnabled = true;
            environmentConfiguration.ApplicationBasePath = TempDir.FullName;
            environmentConfiguration.ContentBasePath = TempDir.FullName;
            environmentConfiguration.PublicHostname = "localhost";
        }

        public void Dispose() => Port?.Dispose();
    }
}