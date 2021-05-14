using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Milou.Deployer.Web.Core.Agents.Pools
{
    [JsonConverter(typeof(AgentPoolIdConverter))]
    public sealed record AgentPoolId
    {
        public AgentPoolId([JetBrains.Annotations.NotNull] string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
            }

            Value = value;
        }

        public string Value { get; }

        public static AgentPoolId Empty { get; } = new("N/A");

        public override string ToString() => Value;

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