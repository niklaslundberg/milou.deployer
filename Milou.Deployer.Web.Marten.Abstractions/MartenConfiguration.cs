using System.Globalization;
using System.Linq;
using Arbor.AppModel.Configuration;
using Arbor.AppModel.ExtensionMethods;
using Arbor.KVConfiguration.Urns;
using JetBrains.Annotations;

namespace Milou.Deployer.Web.Marten.Abstractions
{
    [Urn(MartenConstants.MartenConfiguration)]
    [UsedImplicitly]
    [Optional]
    public class MartenConfiguration : IConfigurationValues
    {
        public MartenConfiguration(string connectionString, bool enabled = false)
        {
            ConnectionString = connectionString;
            Enabled = enabled;
        }

        public string ConnectionString { get; }

        public bool Enabled { get; }

        public bool IsValid => !Enabled || !string.IsNullOrWhiteSpace(ConnectionString);

        public override string ToString() =>
            $"{nameof(ConnectionString)}: [{ConnectionString.MakeKeyValuePairAnonymous(ArborStringExtensions.DefaultAnonymousKeyWords.ToArray())}], {nameof(Enabled)}: {Enabled.ToString(CultureInfo.InvariantCulture).ToLowerInvariant()}";
    }
}