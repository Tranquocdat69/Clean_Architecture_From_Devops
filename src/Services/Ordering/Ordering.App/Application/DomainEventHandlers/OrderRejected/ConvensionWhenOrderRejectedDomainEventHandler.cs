namespace ECom.Services.Ordering.App.Application.DomainEventHandlers.OrderRejected
{
    public class ConvensionWhenOrderRejectedDomainEventHandler : IDomainEventHandler<OrderRejectedDomainEvent>
    {
        private readonly IPublisher<ProducerData<string, string>> _publisher;
        private const string c_keyCommand = "convension";

        public ConvensionWhenOrderRejectedDomainEventHandler(IPublisher<ProducerData<string, string>> IKafkaProducer)
        {
            _publisher = IKafkaProducer;
        }
        public Task Handle(OrderRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(notification.CompensionTopic))
            {
                IIntegrationEvent IntegrationEvents;
                switch (notification.CompensionTopic)
                {
                    case "balance":
                        IntegrationEvents = new UpdateCreditLimitIntegrationEventsEvent(
                            userId: notification.CustomerId,
                            totalCost: notification.TotalCost,
                            replyAddress: ""
                        );
                        break;
                    case "catalog":
                        IntegrationEvents = new UpdateProductAvaibleStockIntegrationEventsEvent(
                            items: notification.Items,
                            replyAddress: ""
                        );
                        break;
                    default:
                        throw new OrderingDomainException("Undefined arrgument "+nameof(notification.CompensionTopic));
                }
                string messageValue = JsonSerializer.Serialize(IntegrationEvents);
                ProducerData<string, string> produceData = new ProducerData<string, string>(
                value: messageValue,
                key: c_keyCommand,
                topic: notification.CompensionTopic);
                _publisher.Publish(produceData);

            }
            return Task.CompletedTask;
        }
    }
}
