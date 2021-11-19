using Arbor.AppModel.Caching;
using Arbor.AppModel.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Milou.Deployer.Web.IisHost.Areas.Caching
{
    [UsedImplicitly]
    public class InMemoryCacheModule : IModule
    {
        public IServiceCollection Register(IServiceCollection builder) => builder
                                                                         .AddSingleton<IMemoryCache, MemoryCache>(
                                                                              new MemoryCache(new MemoryCacheOptions()),
                                                                              this).AddSingleton<ICustomMemoryCache,
                                                                              CustomMemoryCache>(this)
                                                                         .AddSingleton<CurrentCacheVersion>();
    }
}