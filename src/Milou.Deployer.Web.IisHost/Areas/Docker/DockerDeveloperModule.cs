﻿#if DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Arbor.AspNetCore.Host;
using Arbor.Docker;
using JetBrains.Annotations;
using Serilog;

namespace Milou.Deployer.Web.IisHost.Areas.Docker
{
    [UsedImplicitly]
    public class DockerDeveloperModule : IPreStartModule, IAsyncDisposable
    {
        private readonly ILogger _logger;
        private DockerContext _dockerContext;
        private bool _isDisposed;
        private bool _isDisposing;

        public DockerDeveloperModule(ILogger logger) => _logger = logger;

        public async ValueTask DisposeAsync()
        {
            if (_isDisposing || _isDisposed)
            {
                return;
            }

            _isDisposing = true;

            await _dockerContext.DisposeAsync();

            _logger.Information("Disposed DockerContext");

            _isDisposed = true;
            _isDisposing = false;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var dockerArgs = new List<ContainerArgs>();

            var smtp4Dev = new ContainerArgs(
                "rnwood/smtp4dev:linux-amd64-v3",
                "smtp4devtest",
                 new List<PortMapping>
                 {
                     PortMapping.MapSinglePort(3125, 80),
                     PortMapping.MapSinglePort(2526, 25)
                 },
                new Dictionary<string, string> {["ServerOptions:TlsMode"] = "None"}
            );

            var postgresVariables = new Dictionary<string, string> {["POSTGRES_PASSWORD"] = "test"};

            string[] postgresArgs = {"-v", "deploydata:/var/lib/postgresql/data"};

            var postgres = new ContainerArgs(
                "postgres",
                "postgres-deploy",
                new List<PortMapping>
                {
                    PortMapping.MapSinglePort(5433, 5432)
                },
                postgresVariables,
                postgresArgs
            );

            var ftpVariables = new Dictionary<string, string>
            {
                ["FTP_USER"] = "testuser",
                ["FTP_PASS"] = "testpw",
            };

            var passivePorts = new PortRange(21100, 21110);

            var ftpPorts = new List<PortMapping>
            {
                PortMapping.MapSinglePort(20, 20),
                PortMapping.MapSinglePort(21, 21),
                new PortMapping(passivePorts, passivePorts)
            };

            var ftp = new ContainerArgs(
                "fauria/vsftpd",
                "ftp",
                ftpPorts,
                ftpVariables
            );

            dockerArgs.Add(smtp4Dev);
            dockerArgs.Add(postgres);
            dockerArgs.Add(ftp);

            _dockerContext = await DockerContext.CreateContextAsync(dockerArgs, _logger);

            await _dockerContext.ContainerTask;

            _logger.Debug("Started containers {Containers}",
                string.Join(", ", _dockerContext.Containers.Select(container => container.Name)));
        }
    }
}
#endif