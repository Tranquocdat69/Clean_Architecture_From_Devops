namespace FPTS.FIT.BDRD.Services.Ordering.App.Application.RingEventHandlers.CreateOrder
{
    public class CheckProductAvaibleStockHandler : IRingHandler<CreateOrderRingEvent>
    {
        private readonly IPublisher<ProducerData<string, string>> _publisher;
        private readonly string _topic;
        private const string c_keyCommand = "command";

        public CheckProductAvaibleStockHandler(IPublisher<ProducerData<string, string>> IKafkaProducer, string topic)
        {
            _publisher         = IKafkaProducer;
            _topic             = topic;
        }

        public void OnEvent(CreateOrderRingEvent data, long sequence, bool endOfBatch)
        {
            ProducerData<string, string> produceData = new ProducerData<string, string>(
               value: data.JsonData["catalog"],
               key: c_keyCommand + data.BalanceRequestId,
               topic: _topic);
            _publisher.Publish(produceData);
        }
    }
}
