using Arbor.AppModel.Configuration;
using Xunit;

namespace Milou.Deployer.Web.Tests.Unit
{
    public class ConfigurationKeyAnonymousTest
    {
        [Fact]
        public void PasswordShouldBeHidden()
        {
            var configurationKeyInfo = new ConfigurationKeyInfo("password", "abc123");

            Assert.Equal("password", configurationKeyInfo.Key);
            Assert.Equal("*****", configurationKeyInfo.Value);
        }
    }
}