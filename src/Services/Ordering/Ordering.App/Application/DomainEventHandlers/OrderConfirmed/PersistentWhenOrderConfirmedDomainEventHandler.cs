namespace ECom.Services.Ordering.App.Application.DomainEventHandlers.OrderConfirmed
{
    public class PersistentWhenOrderConfirmedDomainEventHandler : IDomainEventHandler<OrderConfirmedDomainEvent>
    {
        private readonly IPublisher<Null, string> _publisher;
        private readonly string _persistentTopic;

        public PersistentWhenOrderConfirmedDomainEventHandler(IPublisher<Null, string> publisher, IConfiguration configuration)
        {
            _publisher   = publisher;
            _persistentTopic = configuration.GetSection("Kafka")["PersistentTopic"];
        }
        public Task Handle(OrderConfirmedDomainEvent notification, CancellationToken cancellationToken)
        {
            _publisher.Produce(new Message<Null, string> { Value = notification.Order.ToString() }, _persistentTopic, 0);
            return Task.CompletedTask;
        }
    }
}
