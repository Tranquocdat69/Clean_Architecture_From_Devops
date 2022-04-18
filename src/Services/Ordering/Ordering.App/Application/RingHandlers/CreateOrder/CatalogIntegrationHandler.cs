namespace ECom.Services.Ordering.App.Application.RingHandlers.CreateOrder
{
    public class CatalogIntegrationHandler : IRingHandler<CreateOrderEvent>
    {
        private const string PRODUCE_TOPIC = "catalog-topic-command";
        private readonly KafkaProducer<string, string> _kafkaProducer;
        private readonly string _replyAddress;

        public CatalogIntegrationHandler(KafkaProducer<string, string> kafkaProducer, string replyAddress)
        {
            _kafkaProducer     = kafkaProducer;
            _replyAddress      = replyAddress;
        }
        public void OnEvent(CreateOrderEvent data, long sequence, bool endOfBatch)
        {
            var integration = new UpdateProductAvaibleStockIntegration(
                productIds: data.OrderItems.Select(x => x.ProductId),
                replyAddress: _replyAddress,
                requestId: data.CatalogRequestId
                );
            _kafkaProducer.Produce(new Message<string, string> { Value = integration.ToString() }, PRODUCE_TOPIC);
        }
    }
}
