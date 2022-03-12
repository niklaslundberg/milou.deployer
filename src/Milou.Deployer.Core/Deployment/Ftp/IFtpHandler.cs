using System;
using System.Collections.Immutable;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Milou.Deployer.Core.Deployment.Ftp
{
    public interface IFtpHandler : IDisposable
    {
        Task CreateDirectoryAsync(FtpPath directoryPath, CancellationToken cancellationToken);
        Task DeleteDirectoryAsync(FtpPath path, CancellationToken cancellationToken);

        Task DeleteFileAsync(FtpPath filePath, CancellationToken cancellationToken);
        Task<bool> DirectoryExistsAsync(FtpPath dir, CancellationToken cancellationToken);
        Task<bool> FileExistsAsync(FtpPath filePath, CancellationToken cancellationToken);

        Task<ImmutableArray<FtpPath>> ListDirectoryAsync(FtpPath path,
            CancellationToken cancellationToken = default);

        Task<DeploySummary> PublishAsync(RuleConfiguration ruleConfiguration,
            DirectoryInfo sourceDirectory,
            CancellationToken cancellationToken);

        Task<DeploySummary> UploadDirectoryAsync(RuleConfiguration ruleConfiguration,
            DirectoryInfo sourceDirectory,
            DirectoryInfo baseDirectory,
            FtpPath basePath,
            CancellationToken cancellationToken);

        Task UploadFileAsync(FtpPath filePath,
            FileInfo sourceFile,
            CancellationToken cancellationToken = default);
    }
}