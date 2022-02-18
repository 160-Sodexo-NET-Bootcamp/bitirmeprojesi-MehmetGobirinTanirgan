using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PCS.AccountUnlockWorkerService.Extensions;

namespace PCS.AccountUnlockWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;
                    services.ConfigureSettings(configuration);
                    services.AddHangfireWithServer(configuration);
                    services.AddHostedService<AccountUnlocker>();
                });
    }
}
