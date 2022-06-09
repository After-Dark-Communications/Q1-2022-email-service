using Confluent.Kafka;
using EmailService.UserSecrets;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace EmailService.Singletons
{
    public class KafkaConsumerHandler : IHostedService
    {
        private readonly IOptions<Security> security;

        public KafkaConsumerHandler(IOptions<Security> security)
        {
            this.security = security;
        }

        private readonly string topic = "mockTopic";
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
                        Debug.WriteLine($"Message: {consumer.Message.Value} received from {consumer.TopicPartitionOffset}");
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
