using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PCS.BackgroundJobs.Jobs.Abstract;
using PCS.BackgroundJobs.Jobs.Concrete;
using PCS.Core.Settings;

namespace PCS.EmailWorkerService
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
                    services.Configure<SmtpSettings>(configuration.GetSection(nameof(SmtpSettings)));
                    services.Configure<AppDbSettings>(configuration.GetSection(nameof(AppDbSettings)));
                    services.AddSingleton<IScanEmailTableJob, ScanEmailTableJob>();
                    services.AddHostedService<EmailScanner>();
                });
    }
}
