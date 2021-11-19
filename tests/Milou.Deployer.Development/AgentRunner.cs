using System;
using System.Threading;
using System.Threading.Tasks;
using Arbor.AppModel.ExtensionMethods;

namespace Milou.Deployer.Development
{
    public class AgentRunner
    {
        private readonly Task<int> _appTask;

        public AgentRunner(CancellationTokenSource cancellationTokenSource, Task<int> appTask)
        {
            _appTask = appTask;
            CancellationTokenSource = cancellationTokenSource;
        }

        public CancellationTokenSource CancellationTokenSource { get; }

        public async Task StopAsync()
        {
            try
            {
                CancellationTokenSource.Cancel();

                await _appTask;
            }
            catch (Exception ex) when (!ex.IsFatal())
            {
                // ignore
            }
        }
    }
}