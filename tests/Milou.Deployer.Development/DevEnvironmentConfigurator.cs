#if DEBUG
using System.IO;
using System.Linq;
using Arbor.Aesculus.Core;
using Arbor.AppModel.Application;
using Arbor.AppModel.Configuration;
using Arbor.KVConfiguration.Urns;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Development
{
    public class DevEnvironmentConfigurator : IConfigureEnvironment
    {
        private readonly AgentId? _agentId;

        public DevEnvironmentConfigurator(ConfigurationInstanceHolder holder) =>
            _agentId = holder.GetInstances<AgentId>().SingleOrDefault().Value;

        public void Configure(EnvironmentConfiguration environmentConfiguration)
        {
            string webProjectPath = Path.Combine(VcsPathHelper.FindVcsRootPath(), "src", "Milou.deployer.Web.IisHost");

            environmentConfiguration.ContentBasePath = webProjectPath;
            environmentConfiguration.IsDevelopmentMode = true;

            if (_agentId is { })
            {
                environmentConfiguration.ApplicationName = _agentId.Value;
            }
        }
    }
}
#endif