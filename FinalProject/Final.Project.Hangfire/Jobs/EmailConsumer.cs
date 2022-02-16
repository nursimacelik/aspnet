using Hangfire;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Final.Project.Domain.Entities;
using Final.Project.Core.EmailServices;

namespace Final.Project.Hangfire.Jobs
{
    public class EmailConsumer
    {
        public EmailConsumer()
        {
            RecurringJob.AddOrUpdate(() => Consume(), "* * * * * *", TimeZoneInfo.Local);
        }

        public void Consume()
        {
            // Connect to Rabbitmq
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "Email", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var email = JsonConvert.DeserializeObject<Email>(message);
                SendEmail(email);

            };

            channel.BasicConsume("Email", autoAck: true, consumer: consumer);
        }

        public void SendEmail(Email email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Nursima Kaya", "nursimacelik00@gmail.com"));
            message.To.Add(new MailboxAddress(email.ReceiverName, email.ReceiverAddress));
            message.Subject = email.Subject;

            message.Body = new TextPart("plain")
            {
                Text = email.Text
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("nursimacelik00@gmail.com", "sodexobootcamp");

                    client.Send(message);
                    //email.DeliveryStatus = "Successfull";
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                email.TryCount++;
                if(email.TryCount < 5)
                {
                    EmailService.AddEmailToQueue(email);
                }
            }
        }
    }
}
