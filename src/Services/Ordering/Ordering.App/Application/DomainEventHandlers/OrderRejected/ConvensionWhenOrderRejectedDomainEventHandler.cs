namespace ECom.Services.Ordering.App.Application.DomainEventHandlers.OrderRejected
{
    public class ConvensionWhenOrderRejectedDomainEventHandler : IDomainEventHandler<OrderRejectedDomainEvent>
    {
        private readonly IPublisher<string, string> _publisher;
        private const string KEY_COMMAND = "convension";

        public ConvensionWhenOrderRejectedDomainEventHandler(IPublisher<string, string> IKafkaProducer)
        {
            _publisher = IKafkaProducer;
        }
        public Task Handle(OrderRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(notification.CompensionTopic))
            {
                IIntegration integration;
                switch (notification.CompensionTopic)
                {
                    case "Balance":
                        integration = new UpdateCreditLimitIntegrationEvent(
                            userId: notification.CustomerId,
                            totalCost: notification.TotalCost,
                            replyAddress: ""
                        );
                        break;
                    case "Catalog":
                        integration = new UpdateProductAvaibleStockIntegrationEvent(
                            items: notification.Items,
                            replyAddress: ""
                        );
                        break;
                    default:
                        throw new OrderingDomainException("Undefined arrgument "+nameof(notification.CompensionTopic));
                }
                string messageValue = JsonSerializer.Serialize(integration);
                _publisher.Produce(new Message<string, string> { Value = messageValue, Key = KEY_COMMAND }, notification.CompensionTopic);

            }
            return Task.CompletedTask;
        }
    }
}
