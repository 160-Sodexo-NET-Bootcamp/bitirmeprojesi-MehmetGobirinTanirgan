using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PCS.Api.AutofacModules;
using Serilog;
using Serilog.Events;

namespace PCS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                  .WriteTo.Console(LogEventLevel.Information)
                  .WriteTo.Debug(LogEventLevel.Information)
                  .WriteTo.File("Logs/logs-.txt", LogEventLevel.Error,
                  rollingInterval: RollingInterval.Day)
                  .CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .UseSerilog()
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ServiceModule()))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
