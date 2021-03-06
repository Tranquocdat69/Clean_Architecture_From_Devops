using ECom.Services.Balance.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ECom.Services.Balance.App.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IHost MigrateDbContext<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<TContext>();
                var configuration = services.GetService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    InvokeSeeder(seeder, context, services, configuration);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                    throw;
                }
            }
            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext? context, IServiceProvider serviceProvider, IConfiguration configuration)
            where TContext : DbContext
        {
            var connectionString = configuration.GetConnectionString(ServiceCollectionExtensions.DB_CONNECTION_KEY);

            if (context != null)
            {
                if (!String.IsNullOrEmpty(connectionString))
                {
                    context.Database.Migrate();
                }
                seeder(context, serviceProvider);
            }
        }

        public static IHost InitConsumeMessageFromTopicKafka(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                InvokeConsumer(serviceProvider);
            }

            return host;
        }

        private static void InvokeConsumer(IServiceProvider serviceProvider)
        {
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            string topic = configuration.GetSection("Kafka").GetSection("CommandTopic").Value;
            int numberOfDeserializeHandlers = Int32.Parse(configuration.GetSection("Disruptor").GetSection("NumberOfDeserializeHandlers").Value);
            int partition = 0;
            int currentDesializeHandler = 0;
            var consumeService = serviceProvider.GetRequiredService<IKafkaSubcriberService<string, string>>();
            var inputRing = serviceProvider.GetRequiredService<RingBuffer<UpdateCreditLimitRingEvent>>();

            StartConsumeThenPublishMessageIntoRing(
                consumeService,
                inputRing,
                currentDesializeHandler,
                numberOfDeserializeHandlers,
                topic,
                UserDbContextSeed.CurrentCommandTopicOffset,
                partition);
        }

        private static void StartConsumeThenPublishMessageIntoRing(
            IKafkaSubcriberService<string, string> consumeService,
            RingBuffer<UpdateCreditLimitRingEvent> inputRing,
            int currentDesializeHandler,
            int numberOfDeserializeHandlers,
            string topic,
            long currentOffset,
            int partition)
        {
            consumeService.StartConsumeTask(record =>
            {
                if (record != null)
                {
                    long sequence = inputRing.Next();
                    var data = inputRing[sequence];
                    data.Offset = record.Offset.Value;
                    data.IsCompensatedMessage = record.Message.Key.Contains("command") ? false : true;
                    data.UpdateCreditLimitCommandString = record.Message.Value;
                    data.RequestId = record.Message.Key.Replace("command", "");
                    data.DeserializeHandlerId = GetCurrentDeserializeHandlerId(ref currentDesializeHandler, numberOfDeserializeHandlers);
                    data.SequenceRing = sequence;
                    inputRing.Publish(sequence);
                }
            }, topic, currentOffset, partition, default);
        }

        private static int GetCurrentDeserializeHandlerId(ref int currentDesializeHandler, int numberOfDeserializeHandlers)
        {
            currentDesializeHandler++;
            if (currentDesializeHandler > numberOfDeserializeHandlers)
            {
                currentDesializeHandler = 1;
            }

            return currentDesializeHandler;
        }
    }
}
