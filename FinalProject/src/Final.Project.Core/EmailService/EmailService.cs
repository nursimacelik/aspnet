using Final.Project.Domain.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Final.Project.Core.EmailServices
{
    public static class EmailService
    {
        public static void AddEmailToQueue(Email email)
        {
            // Connect to Rabbitmq
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" }; 
            factory.AutomaticRecoveryEnabled = true;
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Email", durable: true, exclusive: false, autoDelete: false, arguments: null);

                // Serialize email and convert to bytes
                string message = JsonConvert.SerializeObject(email);
                var messageBody = Encoding.UTF8.GetBytes(message);

                // Publish to "Email" topic
                channel.BasicPublish(exchange: "", routingKey: "Email", body: messageBody);
            }
        }
    }
}
