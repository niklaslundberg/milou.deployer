using System.Collections.Generic;
using Arbor.KVConfiguration.Core;
using Arbor.KVConfiguration.Core.Metadata;
using Serilog.Events;

namespace Milou.Deployer.Web.Agent
{
    public class AgentConfigurationView
    {
        public Dictionary<string, string> EnvironmentVariables { get; init; }

        public LogEventLevel CurrentLogLevel { get; init; }

        public MultipleValuesStringPair[] ConfigurationItems { get; init; }
    }
}
