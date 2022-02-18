using Newtonsoft.Json;
using PCS.Core.Settings;
using PCS.Entity.Models;
using RabbitMQ.Client;
using System.Text;

namespace PCS.Service.RabbitMq.Publishers
{
    public static class AccountUnlockJobPublisher
    {
        public static void Publish(LockoutRecord lockoutRecord, RabbitMqSettings rabbitMqSettings)
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = rabbitMqSettings.Hostname, 
                UserName = rabbitMqSettings.Username, 
                Password = rabbitMqSettings.Password 
            };
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue: "LockoutRecordQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            //IBasicProperties basicProperties = channel.CreateBasicProperties();
            //basicProperties.Persistent = true;
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(lockoutRecord));
            channel.BasicPublish(exchange: "", routingKey: "LockoutRecordQueue", basicProperties: null, body: byteData);
        }
    }
}
