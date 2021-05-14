using System.ComponentModel.DataAnnotations;
using Arbor.App.Extensions.Messaging;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Agents.Commands
{
    public sealed record CreateAgentInstallConfiguration
        ([Required] AgentId AgentId) : ICommand<AgentInstallConfiguration>;
}