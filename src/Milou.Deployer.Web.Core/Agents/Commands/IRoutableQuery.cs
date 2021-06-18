using Arbor.Hypermedia;
using MediatR;

namespace Milou.Deployer.Web.Core.Agents.Commands
{
    public interface IRoutableQuery<out T> : IRequest<IQueryResult<T>> where T : IMetadata
    {
    }
}