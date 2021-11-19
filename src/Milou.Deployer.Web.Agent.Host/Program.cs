using System.Threading.Tasks;
using Arbor.AppModel;
using Arbor.Primitives;
using Milou.Deployer.Web.Agent.Host.Configuration;

namespace Milou.Deployer.Web.Agent.Host
{
    internal static class Program
    {
        public static Task<int> Main(string[] args) => AppStarter<AgentStartup>.StartAsync(
            args,
            EnvironmentVariables.GetEnvironmentVariables().Variables);
    }
}