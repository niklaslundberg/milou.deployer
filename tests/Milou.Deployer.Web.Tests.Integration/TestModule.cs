using Arbor.AppModel.DependencyInjection;
using Arbor.AppModel.Time;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Milou.Deployer.Web.Tests.Integration
{
    [UsedImplicitly]
    public class TestModule : IModule
    {
        public IServiceCollection Register(IServiceCollection builder) =>
            builder.AddSingleton(new TimeoutConfiguration {CancellationEnabled = false});
    }
}