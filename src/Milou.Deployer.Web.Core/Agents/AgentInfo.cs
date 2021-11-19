using System;
using Arbor.AppModel.Messaging;
using Arbor.Hypermedia;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Agents
{
    public class AgentInfo : IQueryResult, IEntity

    {
    public AgentInfo(AgentId agentId,
        DateTimeOffset? connectedAt = null,
        string? connectionId = null,
        string? currentDeploymentTaskId = null,
        DeploymentTargetId? currentDeploymentTargetId = null,
        AgentConfigurationView? agentConfigView = null)
    {
        AgentId = agentId;
        ConnectedAt = connectedAt;
        ConnectionId = connectionId;
        CurrentDeploymentTaskId = currentDeploymentTaskId;
        CurrentDeploymentTargetId = currentDeploymentTargetId;
        AgentConfigView = agentConfigView;
    }

    public AgentId AgentId { get; }

    public DateTimeOffset? ConnectedAt { get; }

    public string? ConnectionId { get; }

    public string? CurrentDeploymentTaskId { get; }

    public DeploymentTargetId? CurrentDeploymentTargetId { get; }

    public AgentConfigurationView? AgentConfigView { get; }

    public EntityContext Context => new(AgentId.Value, nameof(Agent));
    }
}