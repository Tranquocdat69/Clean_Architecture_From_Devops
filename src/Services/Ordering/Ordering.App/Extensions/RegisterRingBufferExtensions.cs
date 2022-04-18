using ECom.Services.Ordering.App.Application.RingHandlers.CreateOrder;
using ECom.Services.Ordering.App.Application.RingHandlers.CreateOrderEvent;

namespace ECom.Services.Ordering.App.Extensions
{
    public static class RegisterRingBufferExtensions
    {
        public static IServiceCollection AddOrderRingBuffer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp =>
            {
                var mediator = sp.GetRequiredService<IMediator>();
                var disruptor = new Disruptor.Dsl.Disruptor<CreateOrderEvent>(() => new CreateOrderEvent(), 2048, TaskScheduler.Current, producerType: ProducerType.Multi, new BlockingWaitStrategy());
                var logger = sp.GetRequiredService<ILogger<LogHandler>>();
                disruptor.HandleEventsWith(new LogHandler())
                .HandleEventsWith(new BusinessHandler())
                .HandleEventsWith(new CustomerIntegrationHandler(), new CatalogIntegrationHandler())
                .HandleEventsWith(new PersistentHandler());
                return disruptor.Start();
            });

            return services;
        }
    }
}
