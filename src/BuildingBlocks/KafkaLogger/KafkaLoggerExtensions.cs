using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ECom.BuildingBlocks.LogLib.KafkaLogger.Configs;

namespace ECom.BuildingBlocks.LogLib.KafkaLogger
{
    public static class KafkaLoggerExtensions
    {
        public static ILoggingBuilder AddKafkaLogger(this ILoggingBuilder builder, Action<KafkaLoggerConfiguration> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, KafkaLoggerProvider>();
            builder.Services.Configure<KafkaLoggerConfiguration>(configure);
            builder.Services.AddSingleton(sp =>
            {
                var bootstrapSevers = sp.GetRequiredService<IOptions<KafkaLoggerConfiguration>>().Value.BootstrapServers;
                ProducerConfig producerConfig = new ProducerConfig()
                {
                    BootstrapServers = bootstrapSevers,
                    QueueBufferingMaxMessages = 2000000,
                    RetryBackoffMs = 500,
                    MessageSendMaxRetries = 3,
                    LingerMs = 5
                };
                var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
                return new KafkaLoggerProducer(producer);
            });
            return builder;
        }
    }
}
