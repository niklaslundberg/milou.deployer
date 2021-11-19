using Arbor.AppModel.Messaging;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Agents.Pools
{
    public sealed record AssignTargetToPool
        (AgentPoolId AgentPoolId, DeploymentTargetId DeploymentTargetId) : ICommand<AssignTargetToPoolResult>;
}