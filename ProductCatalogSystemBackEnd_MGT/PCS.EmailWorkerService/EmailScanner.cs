using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PCS.BackgroundJobs.Jobs.Abstract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PCS.EmailWorkerService
{
    public class EmailScanner : BackgroundService
    {
        private readonly ILogger<EmailScanner> _logger;
        private readonly IScanEmailTableJob scanEmailTableJob;

        public EmailScanner(ILogger<EmailScanner> logger, IScanEmailTableJob scanEmailTableJob)
        {
            _logger = logger;
            this.scanEmailTableJob = scanEmailTableJob;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await scanEmailTableJob.ScanEmailsAndSendAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{nameof(EmailScanner)} => Exception message: {ex.Message}");
                }
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
