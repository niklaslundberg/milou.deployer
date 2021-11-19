using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Arbor.AppModel.Startup;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Milou.Deployer.Web.Agent;

namespace Milou.Deployer.Web.IisHost.AspNetCore.Startup
{
    [UsedImplicitly]
    public class StartupTasksMiddleware
    {
        private static readonly PathString HubPath = new(AgentConstants.HubRoute);
        private static readonly PathString AgentTaskResultPath = new(AgentConstants.DeploymentTaskResult);
        private readonly StartupTaskContext _context;
        private readonly RequestDelegate _next;
        private readonly PathString _startupSegment = new("/startup");

        public StartupTasksMiddleware(StartupTaskContext context, RequestDelegate next)
        {
            _context = context;
            _next = next;
        }

        [PublicAPI]
        public async Task Invoke(HttpContext httpContext)
        {
            if (_context.IsCompleted ||
                httpContext.Request.Path.StartsWithSegments(_startupSegment, StringComparison.OrdinalIgnoreCase) ||
                httpContext.Request.Path.StartsWithSegments(HubPath) ||
                httpContext.Request.Path.StartsWithSegments(AgentTaskResultPath) ||
                (httpContext.Request.Path.Value is { } pathValue && pathValue.StartsWith("/deployment-task")))
            {
                await _next(httpContext);
            }
            else
            {
                HttpResponse response = httpContext.Response;

                response.StatusCode = (int)HttpStatusCode.TemporaryRedirect;

                response.Headers.TryAdd("location", _startupSegment.Value);
            }
        }
    }
}