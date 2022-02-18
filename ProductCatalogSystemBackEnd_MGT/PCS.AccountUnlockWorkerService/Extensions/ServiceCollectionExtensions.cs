using Hangfire;
using Hangfire.MySql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PCS.Core.Settings;
using System;

namespace PCS.AccountUnlockWorkerService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppDbSettings>(configuration.GetSection(nameof(AppDbSettings)));
            services.Configure<RabbitMqSettings>(configuration.GetSection(nameof(RabbitMqSettings)));
            return services;
        }

        public static IServiceCollection AddHangfireWithServer(this IServiceCollection services, IConfiguration configuration)
        {
            var hangfireDbSettings = configuration.GetSection(nameof(HangfireDbSettings)).Get<HangfireDbSettings>();
            _ = hangfireDbSettings.Provider switch
            {
                "MsSql" => services.AddHangfire(config => config.UseSqlServerStorage(hangfireDbSettings.MsSqlConStr)),
                "MySql" => services.AddHangfire(config => config.UseStorage(new MySqlStorage(hangfireDbSettings.MySqlConStr, new MySqlStorageOptions()))),
                _ => throw new Exception($"{nameof(AccountUnlockWorkerService)} => Unsupported provider: {hangfireDbSettings.Provider}")
            };
            services.AddHangfireServer();
            return services;
        }
    }
}
