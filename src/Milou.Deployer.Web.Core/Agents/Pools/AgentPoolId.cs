using System;
using System.Diagnostics.CodeAnalysis;
using Arbor.ModelBinding.Primitives;
using Newtonsoft.Json;

namespace Milou.Deployer.Web.Core.Agents.Pools
{
    [StringValueType(StringComparison.OrdinalIgnoreCase)]
    public partial class AgentPoolId
    {
        public static AgentPoolId Empty { get; } = new("N/A");

        public static bool TryParse(string? value, [NotNullWhen(true)] out AgentPoolId? agentPoolId)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                agentPoolId = default;

                return false;
            }

            agentPoolId = new AgentPoolId(value);

            return true;
        }
    }
}
