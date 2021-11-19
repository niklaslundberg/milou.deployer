using Arbor.AppModel.Application;
using Arbor.AppModel.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Milou.Deployer.Web.Core.Deployment.Targets;

namespace Milou.Deployer.Web.Core
{
    [UsedImplicitly]
    public class DataSeederModule : IModule
    {
        private readonly IApplicationAssemblyResolver _applicationAssemblyResolver;

        public DataSeederModule(IApplicationAssemblyResolver applicationAssemblyResolver) =>
            _applicationAssemblyResolver = applicationAssemblyResolver;

        public IServiceCollection Register(IServiceCollection builder) =>
            builder.RegisterAssemblyTypesAsSingletons<IDataSeeder>(_applicationAssemblyResolver.GetAssemblies());
    }
}