using Confluent.Kafka;
using EmailService.IServices;
using EmailService.Models;
using EmailService.Services;
using EmailService.UserSecrets;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Json;

namespace EmailService.Singletons
{
    public class KafkaConsumerHandler : IHostedService
    {
        private readonly IOptions<Security> security;
        private readonly ISendEmailService emailService;
        private readonly String wwwRoot;

        public KafkaConsumerHandler(IOptions<Security> security, ISendEmailService emailService, IWebHostEnvironment environment)
        {
            this.security = security;
            this.emailService = emailService;
            this.wwwRoot = environment.WebRootPath;
        }

        private readonly string topic = "dinnerinmotion.reservations.create";
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = security.Value.KafkaGroupId,
                BootstrapServers = security.Value.KafkaServer,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = security.Value.KafkaUsername,
                SaslPassword = security.Value.KafkaPassword
            };

            using (var builder = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                builder.Subscribe(topic);
                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        var consumer = builder.Consume(cancelToken.Token);
                        Reservation reservation = JsonSerializer.Deserialize<Reservation>(consumer.Message.Value);
                        Debug.WriteLine($"Message: {consumer.Message.Value} received from {consumer.TopicPartitionOffset}");
                        
                        EmailInfo emailInfo = new EmailInfo(security)
                        {
                            Subject = "Survey DinnerInMotion",
                            Username = reservation.name,
                            ReceiverAddress = reservation.email,
                            BodyFormat = Array.Empty<string>(),
                            TemplateFilePath = Path.Combine(wwwRoot, "Templates/DimMail.htm"),
                        };
                        emailService.SendEmail(emailInfo);
                    }
                }
                catch (Exception)
                {
                    builder.Close();
                }
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
