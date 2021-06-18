using Arbor.App.Extensions.Messaging;
using Arbor.Hypermedia;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.Core.Agents.Commands
{
    public interface ICommandResult<out T> : ICommandResult where T : IMetadata
    {
        T Result { get; }
    }
}