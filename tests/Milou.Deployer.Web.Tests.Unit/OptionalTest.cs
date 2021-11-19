using System.Reflection;
using Arbor.AppModel.Configuration;
using Arbor.KVConfiguration.Urns;
using Xunit;

namespace Milou.Deployer.Web.Tests.Unit
{
    public class OptionalTest
    {
        [Fact]
        public void ExcludedAutoRegistrationTypeHasOptionalAttribute()
        {
            var optionalAttribute = typeof(ExcludedAutoRegistrationType).GetCustomAttribute<OptionalAttribute>();

            Assert.NotNull(optionalAttribute);
        }
    }
}