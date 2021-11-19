using Arbor.AppModel.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Milou.Deployer.Web.IisHost.Areas.Security
{
    [UsedImplicitly]
    public class IpModule : IModule
    {
        public IServiceCollection Register(IServiceCollection builder) =>
            builder.AddSingleton<AllowedIpAddressHandler>(this);
    }
}