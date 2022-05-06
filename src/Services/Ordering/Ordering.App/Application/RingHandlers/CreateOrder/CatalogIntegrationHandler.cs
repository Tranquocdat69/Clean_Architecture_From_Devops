namespace ECom.Services.Ordering.App.Application.RingHandlers.CreateOrder
{
    public class CatalogIntegrationHandler : IRingHandler<CreateOrderEvent>
    {
        private readonly IPublisher<string, string> _publisher;
        private readonly string _replyAddress;
        private readonly string _topic;
        private const string KEY_COMMAND = "command";

        public CatalogIntegrationHandler(IPublisher<string, string> IKafkaProducer, string replyAddress, string topic)
        {
            _publisher         = IKafkaProducer;
            _replyAddress      = replyAddress;
            _topic             = topic;
        }

        public void OnEvent(CreateOrderEvent data, long sequence, bool endOfBatch)
        {
            var integration = new UpdateProductAvaibleStockIntegrationEvent(
                items: data.Items,
                replyAddress: _replyAddress
                );
            string messageValue = JsonSerializer.Serialize(integration);
            _publisher.Produce(new Message<string, string> { Value = messageValue, Key = KEY_COMMAND+data.BalanceRequestId}, _topic);
        }
    }
}
