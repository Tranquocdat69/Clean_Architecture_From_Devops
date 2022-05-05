namespace FPTS.FIT.BDRD.Services.Ordering.App.Application.Commands
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IPublisher<ProducerData<string, string>> _publisher;
        private readonly IOrderRepository _repository;
        private readonly string _persistanceTopic;
        private const string c_keyCommand = "DELETE";

        public DeleteOrderCommandHandler(
            IPublisher<ProducerData<string, string>> publisher, 
            IOrderRepository repository,
            IConfiguration configuration)
        {
            _publisher  = publisher;
            _repository = repository;
            _persistanceTopic = configuration.GetSection("Kafka")?["CommandTopic"] ?? "order-persistent-topic";
        }
        public Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var result = _repository.Remove(request.OrderId);
            if (result)
            {
                PublishToKafka(request.OrderId);
            }
            return Task.FromResult(result);
        }

        private void PublishToKafka(string orderId)
        {
            ProducerData<string, string> produceData = new ProducerData<string, string>(
               value: orderId,
               key: c_keyCommand,
               topic: _persistanceTopic);
            _publisher.Publish(produceData);
        }
    }
}
