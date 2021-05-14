using Arbor.App.Extensions.Messaging;

namespace Milou.Deployer.Web.Core.Agents.Commands
{
    public class ResetAgentTokenResult : ICommandResult
    {
        public ResetAgentTokenResult(string accessToken) => AccessToken = accessToken;

        public string AccessToken { get; }
    }
}