using Arbor.App.Extensions.Messaging;
using Serilog.Events;

namespace Milou.Deployer.Web.Agent
{
    public record LogLevelChanged(LogEventLevel NewLevel) : IEvent;
}
