using Arbor.AppModel.DependencyInjection;
using Arbor.AppModel.IO;
using Arbor.KVConfiguration.Core;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Milou.Deployer.Web.Agent.Host.IO
{
    public class TempDirectoryModule : IModule
    {
        private readonly MultiSourceKeyValueConfiguration _keyValueConfiguration;
        private readonly ILogger _logger;

        public TempDirectoryModule(MultiSourceKeyValueConfiguration keyValueConfiguration, ILogger logger)
        {
            _keyValueConfiguration = keyValueConfiguration;
            _logger = logger;
        }

        public IServiceCollection Register(IServiceCollection builder)
        {
            TempPathHelper.SetTempPath(_keyValueConfiguration, _logger);

            return builder;
        }
    }
}