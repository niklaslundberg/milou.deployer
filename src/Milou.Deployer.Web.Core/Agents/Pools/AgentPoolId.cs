﻿using System.Diagnostics.CodeAnalysis;
using Milou.Deployer.Web.Agent;
using Newtonsoft.Json;

namespace Milou.Deployer.Web.Core.Agents.Pools
{
    [JsonConverter(typeof(AgentPoolIdConverter))]
    public sealed class AgentPoolId
    {
        public override string ToString() => Value ?? base.ToString();

        public string Value { get; }

        public AgentPoolId(string value) => Value = value;

        public static bool TryParse(string? value
            , [NotNullWhen(true)] out  AgentPoolId? agentPoolId)
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