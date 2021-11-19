using System;
using Arbor.AppModel.Messaging;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Agents.Commands
{
    public class AgentInstallConfiguration : ICommandResult
    {
        public AgentInstallConfiguration(AgentId agentId, string accessToken, Uri serverUri)
        {
            AgentId = agentId;
            AccessToken = accessToken;
            ServerUri = serverUri;
        }

        public AgentId AgentId { get; }

        public string AccessToken { get; }

        public Uri ServerUri { get; }
    }
}