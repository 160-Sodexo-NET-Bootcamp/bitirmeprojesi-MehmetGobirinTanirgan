using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using PCS.Core.Middlewares;

namespace PCS.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            return app;
        }

        public static IApplicationBuilder UseDefaultHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/api/HealthCheck", new HealthCheckOptions()
            {
                ResponseWriter = async (context, report) =>
                {
                    await context.Response.WriteAsync("Pcs Api Health Check");
                }
            });
            return app;
        }
    }
}
