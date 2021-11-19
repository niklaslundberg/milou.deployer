using System.Linq;
using System.Reflection;
using Arbor.AppModel.ExtensionMethods;
using FluentAssertions;
using Milou.Deployer.Web.Agent;
using Milou.Deployer.Web.IisHost.Areas.Agents;
using Xunit;

namespace Milou.Deployer.Web.Tests.Integration
{
    public class AgentHubTests
    {
        [Fact]
        public void AllConstantNamesShouldExistInAgentHub()
        {
            string[] signalRFieldValues = typeof(AgentConstants).GetFields()
                                                                .Where(field =>
                                                                     field.Name.StartsWith("SignalRAgentHub"))
                                                                .Select(field => field.GetValue(null) as string)
                                                                .NotNull().ToArray();

            string[] hubPublicNames = typeof(AgentHub).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                      .Where(method => !method.IsVirtual).Select(method => method.Name)
                                                      .ToArray();

            foreach (string publicAgentMethodName in signalRFieldValues)
            {
                hubPublicNames.Should().Contain(publicAgentMethodName);
            }
        }
    }
}