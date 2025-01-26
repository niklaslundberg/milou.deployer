using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Arbor.Tooler;
using JetBrains.Annotations;
using Milou.Deployer.Bootstrapper.Common;
using Milou.Deployer.Core;

namespace Milou.Deployer.Bootstrapper.ConsoleClient
{
    public static class Program
    {
        private static TimeSpan GetTimeout(string[] args)
        {
            if (!int.TryParse(
                    args.SingleOrDefault(arg =>
                             arg.StartsWith("timeout-in-seconds=", StringComparison.OrdinalIgnoreCase))
                       ?.Split('=').LastOrDefault(),
                    out int timeoutInSeconds) ||
                timeoutInSeconds <= 0)
            {
                return TimeSpan.FromSeconds(60);
            }

            return TimeSpan.FromSeconds(timeoutInSeconds);
        }

        [PublicAPI]
        private static async Task<int> Main(string[] args)
        {
            int exitCode;

            using (BootstrapperApp bootstrapperApp = await BootstrapperApp.CreateAsync(args).ConfigureAwait(false))
            {
                using var cts = CancellationHelper.CreateCancellationTokenSource(GetTimeout(args));

                NuGetPackageInstallResult nuGetPackageInstallResult =
                    await bootstrapperApp.ExecuteAsync(args.ToImmutableArray(), cts.Token).ConfigureAwait(false);

                exitCode = nuGetPackageInstallResult.SemanticVersion is { } &&
                           nuGetPackageInstallResult.PackageDirectory is { }
                    ? 0
                    : 1;
            }

            return exitCode;
        }
    }
}