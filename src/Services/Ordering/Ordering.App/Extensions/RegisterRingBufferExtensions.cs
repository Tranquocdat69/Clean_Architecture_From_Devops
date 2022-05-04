using ECom.Services.Ordering.App.Application.RingHandlers.CreateOrder;

namespace ECom.Services.Ordering.App.Extensions
{
    public static class RegisterRingBufferExtensions
    {
        public static IServiceCollection AddOrderRingBuffer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp =>
            {
                var mediator = sp.GetRequiredService<IMediator>();
                var ringConfig = configuration.GetSection("Disruptor").Get<RingConfiguration>();
                var disruptor = new Disruptor<CreateOrderRingEvent>(
                    () => new CreateOrderRingEvent(), ringConfig.RingSize, TaskScheduler.Current, producerType: ProducerType.Multi, new BlockingWaitStrategy());
                var commandTopics = configuration.GetSection("Kafka")?.GetSection("CommandTopic");//["Balance"] ?? "balance-command-topic"
                var producer = sp.GetRequiredService<IPublisher<ProducerData<string, string>>>();
                var replyAddress = configuration["ExternalAddress"];
                var balanceCommandTopic = commandTopics?["Balance"] ?? "balance-command-topic";
                var catalogCommandTopic = commandTopics?["Catalog"] ?? "catalog-command-topic";
                disruptor
                .HandleEventsWith(GetSerializeHandlers(replyAddress, ringConfig.NumberOfSerializeHandlers))
                .HandleEventsWith(
                    new BalanceIntegrationEventsHandler(producer, balanceCommandTopic), 
                    new CatalogIntegrationEventsHandler(producer, catalogCommandTopic));
                return disruptor.Start();
            });

            return services;
        }

        private static JsonSerializeHandler[] GetSerializeHandlers(string replyAddress, int numbers)
        {
            JsonSerializeHandler[] arr = new JsonSerializeHandler[numbers];
            for(var i = 0; i < numbers; i++)
            {
                arr[i] = new JsonSerializeHandler(replyAddress, i+1, numbers);
            }
            return arr;
        }
    }

    internal class RingConfiguration
    {
        public int RingSize { get; set; }
        public int NumberOfSerializeHandlers { get; set; }
    }
}
