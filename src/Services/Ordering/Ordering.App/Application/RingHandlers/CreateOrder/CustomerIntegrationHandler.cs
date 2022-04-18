namespace ECom.Services.Ordering.App.Application.RingHandlers.CreateOrder
{
    public class CustomerIntegrationHandler : IRingHandler<CreateOrderEvent>
    {
        private const string PRODUCE_TOPIC = "customer-topic-command";
        private readonly KafkaProducer<string, string> _kafkaProducer;
        private readonly string _replyAddress;

        public CustomerIntegrationHandler(KafkaProducer<string, string> kafkaProducer, string replyAddress)
        {
            _kafkaProducer = kafkaProducer;
            _replyAddress  = replyAddress;
        }
        public void OnEvent(CreateOrderEvent data, long sequence, bool endOfBatch)
        {
            decimal cost = 0;
            foreach(var item in data.OrderItems)
                cost += item.GetUnits() * item.GetUnitPrice();

            var integration = new UpdateCreditLimitIntegration(
                totalCost: cost,
                userId: data.UserId,
                replyAddress: _replyAddress
                );
            _kafkaProducer.Produce(new Message<string, string> { Value = integration.ToString() }, PRODUCE_TOPIC);
        }
    }
}
