using Hangfire;
using Hangfire.MySql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PCS.Core.Settings;
using System;

namespace PCS.HangfireApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HangfireDbSettings>(configuration.GetSection(nameof(HangfireDbSettings)));
            services.Configure<AppDbSettings>(configuration.GetSection(nameof(AppDbSettings)));
            return services;
        }

        public static IServiceCollection AddHangfireWithServer(this IServiceCollection services, IConfiguration configuration)
        {
            var hangfireDbSettings = configuration.GetSection(nameof(HangfireDbSettings)).Get<HangfireDbSettings>();

            _ = hangfireDbSettings.Provider switch
            {
                "MsSql" => services.AddHangfire(config => config.UseSqlServerStorage(hangfireDbSettings.MsSqlConStr)),
                "MySql" => services.AddHangfire(config => config.UseStorage(new MySqlStorage(hangfireDbSettings.MySqlConStr, new MySqlStorageOptions()))),
                _ => throw new Exception($"{nameof(HangfireApi)} => Unsupported provider: {hangfireDbSettings.Provider}")
            };
            services.AddHangfireServer();
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PCS.HangfireApi", Version = "v1" });
            });
            return services;
        }
    }
}
