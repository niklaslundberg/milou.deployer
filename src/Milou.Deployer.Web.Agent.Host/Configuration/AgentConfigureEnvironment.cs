using Arbor.AppModel.Application;
using Arbor.AppModel.Configuration;

namespace Milou.Deployer.Web.Agent.Host.Configuration
{
    [RegistrationOrder(int.MaxValue)]
    public class AgentConfigureEnvironment : IConfigureEnvironment
    {
        public void Configure(EnvironmentConfiguration environmentConfiguration)
        {
            environmentConfiguration.HttpEnabled = false;
            environmentConfiguration.ApplicationName = "TestAgent";
        }
    }
}