using System;
using System.Diagnostics.CodeAnalysis;
using Arbor.Hypermedia;
using Arbor.ModelBinding.Primitives;

namespace Milou.Deployer.Web.Agent
{
    [StringValueType(StringComparison.OrdinalIgnoreCase)]
    public partial class AgentId : IEntity
    {
        public static AgentId Parse([JetBrains.Annotations.NotNull] string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
            }

            bool parsed = TryParse(value, out var agentId);

            if (!parsed)
            {
                throw new FormatException($"Invalid agent id {value}");
            }

            return agentId!;
        }

        public static bool TryParse(string? value, [NotNullWhen(true)] out AgentId? agentId)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                agentId = null;

                return false;
            }

            agentId = new AgentId(value);

            return true;
        }

        private AgentId() : base ("N/A")
        {
        }

        public EntityContext Context => new(Value, nameof(Agent));

        public static AgentId Empty { get; } = new();
    }
}