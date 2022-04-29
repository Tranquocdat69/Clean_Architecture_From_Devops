namespace ECom.Services.Ordering.App.Application.RingHandlers.CreateOrder
{
    public class BalanceIntegrationEventsHandler : IRingHandler<CreateOrderEvent>
    {
        private readonly IPublisher<ProducerData<string, string>> _publisher;
        private readonly string _topic;
        private const string c_keyCommand = "command";

        public BalanceIntegrationEventsHandler(IPublisher<ProducerData<string, string>> publisher, string topic)
        {
            _publisher     = publisher;
            _topic             = topic;
        }
        public void OnEvent(CreateOrderEvent data, long sequence, bool endOfBatch)
        {
            ProducerData<string, string> produceData = new ProducerData<string, string>(
               value: data.JsonData["balance"],
               key: c_keyCommand + data.BalanceRequestId,
               topic: _topic);
            _publisher.Publish(produceData);

        }
    }
}
