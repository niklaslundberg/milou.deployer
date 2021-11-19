using Arbor.AppModel.Cli;
using Arbor.AppModel.Configuration;
using Xunit;

namespace Milou.Deployer.Web.Tests.Unit
{
    public class ParseParameterTests
    {
        [Fact]
        public void ShouldParseValue()
        {
            string[] args = {@"urn:arbor:app:web:application-base-path=C:\Tools\Deployer\"};

            string? result = args.ParseParameter(ConfigurationConstants.ApplicationBasePath);

            Assert.Equal(@"C:\Tools\Deployer\", result);
        }
    }
}