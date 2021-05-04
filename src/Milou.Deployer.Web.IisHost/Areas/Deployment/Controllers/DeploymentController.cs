using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Arbor.App.Extensions.Caching;
using Arbor.App.Extensions.ExtensionMethods;
using Arbor.App.Extensions.Time;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Milou.Deployer.Web.Core.Caching;
using Milou.Deployer.Web.Core.Deployment;
using Milou.Deployer.Web.Core.Deployment.Packages;
using Milou.Deployer.Web.Core.Deployment.Sources;
using Milou.Deployer.Web.IisHost.Areas.Deployment.ViewOutputModels;
using Milou.Deployer.Web.IisHost.Areas.Targets.Controllers;
using Milou.Deployer.Web.IisHost.Controllers;
using Serilog;

namespace Milou.Deployer.Web.IisHost.Areas.Deployment.Controllers
{
    [Area(DeploymentConstants.AreaName)]
    public class DeploymentController : BaseApiController
    {
        private readonly IDeploymentTargetReadService _getTargets;

        [NotNull]
        private readonly ILogger _logger;

        private readonly TimeoutHelper _timeoutHelper;

        public DeploymentController(
            [NotNull] ILogger logger,
            [NotNull] IDeploymentTargetReadService getTargets,
            TimeoutHelper timeoutHelper)
        {
            _logger = logger;
            _getTargets = getTargets ?? throw new ArgumentNullException(nameof(getTargets));
            _timeoutHelper = timeoutHelper;
        }

        [HttpGet]
        [Route("/deployment")]
        public async Task<IActionResult> Index()
        {
            IReadOnlyCollection<DeploymentTarget> targets;
            try
            {
                using CancellationTokenSource cts =
                    _timeoutHelper.CreateCancellationTokenSource(TimeSpan.FromSeconds(30));
                targets =
                    (await _getTargets.GetOrganizationsAsync(cts.Token)).SelectMany(
                        organization => organization.Projects.SelectMany(project => project.DeploymentTargets))
                    .SafeToReadOnlyCollection();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Could not get organizations");
                targets = ImmutableArray<DeploymentTarget>.Empty;
            }

            IReadOnlyCollection<PackageVersion> items = ImmutableArray<PackageVersion>.Empty;

            return View(new DeploymentViewOutputModel(items, targets));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route(TargetConstants.InvalidateCacheRoute, Name = TargetConstants.InvalidateCacheRouteName)]
        public async Task<ActionResult> InvalidateCache(
            [FromBody] InvalidateCache invalidateCache,
            [FromServices] ICustomMemoryCache customMemoryCache,
            [FromServices] IDistributedCache? distributedCache,
            [FromServices] CurrentCacheVersion? currentCacheVersion)
        {
            customMemoryCache.Invalidate(invalidateCache.Prefix);

            if (distributedCache is { } && !string.IsNullOrWhiteSpace(invalidateCache.Prefix))
            {
                try
                {
                    await distributedCache.RemoveAsync(invalidateCache.Prefix);
                }
                catch (Exception ex) when (!ex.IsFatal())
                {
                    _logger.Warning("Could not remove distributed cache with key {Key}", invalidateCache.Prefix);
                }
            }
            else if (currentCacheVersion is {})
            {
                currentCacheVersion.CurrentVersion = new CacheVersion(currentCacheVersion.CurrentVersion.Version + 1);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}