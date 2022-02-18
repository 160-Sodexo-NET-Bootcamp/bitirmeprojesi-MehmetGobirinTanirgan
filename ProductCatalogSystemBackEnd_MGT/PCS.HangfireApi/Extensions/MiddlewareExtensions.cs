using Hangfire;
using Microsoft.AspNetCore.Builder;

namespace PCS.HangfireApi.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                AppPath = "/swagger/index.html",
                DashboardTitle = "PCS Hangfire Dashboard"
            });
            return app;
        }
    }
}
