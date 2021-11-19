using Arbor.AppModel.Messaging;
using Arbor.Hypermedia;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Agents.Commands
{
    public interface IQueryResult<out T> : IQueryResult where T : IMetadata
    {
        T Result { get; }
    }
}