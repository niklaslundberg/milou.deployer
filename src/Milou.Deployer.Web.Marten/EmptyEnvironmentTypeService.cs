using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Milou.Deployer.Web.Core;
using Milou.Deployer.Web.Core.Deployment;

namespace Milou.Deployer.Web.Marten
{
    public class EmptyEnvironmentTypeService : IEnvironmentTypeService
    {
        public Task<ImmutableArray<EnvironmentType>>
            GetEnvironmentTypes(CancellationToken cancellationToken = default) =>
            Task.FromResult(ImmutableArray<EnvironmentType>.Empty);
    }
}