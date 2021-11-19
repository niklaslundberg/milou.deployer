using Arbor.AppModel.Application;
using Arbor.AppModel.Configuration;
using Arbor.KVConfiguration.Urns;
using Xunit;

namespace Milou.Deployer.Web.Tests.Unit
{
    public class HolderTests
    {
        [Fact]
        public void CreateTypeWithSingletons()
        {
            var holder = new ConfigurationInstanceHolder();

            holder.AddInstance(new EnvironmentConfiguration {ApplicationBasePath = @"C:\app"});

            EnvironmentConsumer environmentConsumer = holder.Create<EnvironmentConsumer>();

            Assert.NotNull(environmentConsumer);
            Assert.NotNull(environmentConsumer.EnvironmentConfiguration);
            Assert.Equal(@"C:\app", environmentConsumer.EnvironmentConfiguration.ApplicationBasePath);
        }
    }
}