using Arbor.AppModel.Application;
using Arbor.AppModel.Configuration;
using JetBrains.Annotations;

namespace Milou.Deployer.Web.IisHost.Areas.Configuration
{
    [UsedImplicitly]
    public class ServerConfigureEnvironment : IConfigureEnvironment
    {
        public void Configure(EnvironmentConfiguration environmentConfiguration) =>
            environmentConfiguration.HttpEnabled = true;
    }
}