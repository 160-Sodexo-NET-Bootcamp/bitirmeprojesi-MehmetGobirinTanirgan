using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PCS.BackgroundJobs.Jobs.Concrete;
using PCS.Core.Settings;
using PCS.Entity.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PCS.AccountUnlockWorkerService
{
    public class AccountUnlocker : BackgroundService
    {
        private readonly ILogger<AccountUnlocker> _logger;
        private readonly RabbitMqSettings rabbitMqSettings;
        private IConnection connection;
        private IModel channel;
        private string consumerTag;

        public AccountUnlocker(ILogger<AccountUnlocker> logger, IOptions<RabbitMqSettings> rabbitMqSettingsOptions)
        {
            _logger = logger;
            rabbitMqSettings = rabbitMqSettingsOptions.Value;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //Rabbitmq connection creation
            var connectionFactory = new ConnectionFactory()
            {
                HostName = rabbitMqSettings.Hostname,
                UserName = rabbitMqSettings.Username,
                Password = rabbitMqSettings.Password
            };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "LockoutRecordQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            try
            {
                // Creating a event consumer
                var eventConsumer = new EventingBasicConsumer(channel);
                eventConsumer.Received += (ch, e) =>
                {
                    //Getting data in here
                    var byteData = e.Body.ToArray(); 
                    var lockoutRecord = JsonConvert.DeserializeObject<LockoutRecord>(Encoding.UTF8.GetString(byteData));
                    _logger.LogInformation($"Incoming user id => {lockoutRecord.UserId}");
                    //Register a delayed job that unlocks the account
                    BackgroundJob.Schedule<AccountUnlockJob>(x => x.UnlockAccountAsync(lockoutRecord.UserId), TimeSpan.FromSeconds(lockoutRecord.LockoutUntil.TimeOfDay.TotalSeconds - DateTime.UtcNow.TimeOfDay.TotalSeconds));
                };
                consumerTag = channel.BasicConsume(queue: "LockoutRecordQueue", autoAck: true, consumer: eventConsumer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(AccountUnlocker)} => Exception message: {ex.Message}");
            }
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            //Clear everything if apps shut down
            channel.BasicCancel(consumerTag);
            channel.Close();
            connection.Close();
            channel.Dispose();
            connection.Dispose();
            _logger.LogInformation("Worker stopped at: {time}", DateTimeOffset.Now);
            return base.StopAsync(cancellationToken);
        }
    }
}
