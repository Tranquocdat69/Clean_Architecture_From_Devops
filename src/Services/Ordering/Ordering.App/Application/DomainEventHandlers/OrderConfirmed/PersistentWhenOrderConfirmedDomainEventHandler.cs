namespace ECom.Services.Ordering.App.Application.DomainEventHandlers.OrderConfirmed
#nullable disable
{
    public class PersistentWhenOrderConfirmedDomainEventHandler : IDomainEventHandler<OrderConfirmedDomainEvent>
    {
        private readonly IPublisher<ProducerData<Null, string>> _publisher;
        private readonly string _persistentTopic;

        public PersistentWhenOrderConfirmedDomainEventHandler(IPublisher<ProducerData<Null, string>> publisher, IConfiguration configuration)
        {
            _publisher   = publisher;
            _persistentTopic = configuration.GetSection("Kafka")["PersistentTopic"];
        }
        public Task Handle(OrderConfirmedDomainEvent notification, CancellationToken cancellationToken)
        {
            var message     = JsonSerializer.Serialize(notification.Order);
             var produceData = new ProducerData<Null, string>(message, null, _persistentTopic, 0);

            _publisher.Publish(produceData);
            return Task.CompletedTask;
        }
    }
}
