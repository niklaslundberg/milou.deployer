using System;
using System.Collections.Generic;
using System.Linq;
using Arbor.Hypermedia;
using MediatR;
using Milou.Deployer.Web.Agent;
using Milou.Deployer.Web.Core.Agents.Commands;

namespace Milou.Deployer.Web.Core.Agents.Queries
{
    public class Agent : IMetadata
    {
        private readonly AgentData _agentData;
        private readonly IMediator _mediator;

        public Agent(IMediator mediator, AgentData agentData)
        {
            _mediator = mediator;
            _agentData = agentData;
            AgentId = string.IsNullOrWhiteSpace(_agentData.AgentId) ? AgentId.Empty : new AgentId(_agentData.AgentId);
        }

        public AgentId AgentId { get; }

        public EntityMetadata CreateMetadata() => new AgentMetadata(new AgentView(this), SupportedActions());

        //public Task<ClearAgentWorkTasksResult> Handle(ClearAgentWorkTasks request, CancellationToken cancellationToken) =>
        //    Task.FromResult(new ClearAgentWorkTasksResult(this));

        public bool CanHandle(ClearAgentWorkTasks item) =>
            SupportedActions().Any(action => action is ClearAgentWorkTasks.Metadata);

        public void Handle(ClearAgentWorkTasks clear)
        {
            if (!CanHandle(clear))
            {
                throw new InvalidOperationException($"Cannot handle {clear}");
            }
        }

        public IEnumerable<EntityMetadata> SupportedActions()
        {
            if (AgentId != AgentId.Empty)
            {
                yield return ClearAgentWorkTasks.GetMetadata(AgentId);

                if (_agentData.Enabled)
                {
                    yield return DisableAgent.GetMetadata(AgentId);
                }
            }
        }
        public record AgentView : IEntity
        {
            private readonly AgentId _agentId;

            public AgentView(Agent agent)
            {
                _agentId = agent.AgentId;
                Enabled = agent._agentData.Enabled;
            }

            public bool Enabled { get; }

            public EntityContext Context => new(_agentId.Value, nameof(Agent));
        }
    }
}