using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;

namespace Milou.Deployer.Web.IisHost.AspNetCore.Startup
{
    public class MethodConverterMiddleware
    {
        private readonly RequestDelegate _next;

        public MethodConverterMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase)
                && context.Request.HasFormContentType)
            {
                context.Request.EnableBuffering();

                if (context.Request.Form.TryGetValue("_method", out var values))
                {
                    context.Request.Method = values.ToString();
                }
            }

            await _next.Invoke(context);
        }
    }
}