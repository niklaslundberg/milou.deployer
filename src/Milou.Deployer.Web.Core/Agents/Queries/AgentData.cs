using Marten.Schema;

namespace Milou.Deployer.Web.Core.Agents.Queries
{
    public class AgentData
    {
        [Identity]
        public string AgentId { get; set; }

        public string? AccessToken { get; set; }

        public bool Enabled { get; set; } = true;
    }
}