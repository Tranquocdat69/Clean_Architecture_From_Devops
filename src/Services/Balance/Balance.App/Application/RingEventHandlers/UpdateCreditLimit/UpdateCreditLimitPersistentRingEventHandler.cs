namespace ECom.Services.Balance.App.Application.RingEventHandlers.UpdateCreditLimit
{
    public class UpdateCreditLimitPersistentRingEventHandler : IRingHandler<UpdateCreditLimitPersistentRingEvent>
    {
        private readonly IPublisher<ProducerData<Null, string>> _producer;
        private readonly string _balancePersistentTopic;
        private Dictionary<int, string> _dicJsondata = new();
        private const int MaxBatchSize = 24;
        private const int PartitionId = 0;
        private int _currentBatchSize = 0;

        public UpdateCreditLimitPersistentRingEventHandler(IPublisher<ProducerData<Null, string>> producer, IConfiguration configuration)
        {
            _producer = producer;
            _balancePersistentTopic = configuration.GetSection("Kafka").GetSection("PersistentTopic").Value;
        }
        public void OnEvent(UpdateCreditLimitPersistentRingEvent data, long sequence, bool endOfBatch)
        {
            if (_dicJsondata.ContainsKey(data.UserId))
            {
                _dicJsondata[data.UserId] = data.UpdateCreditLimitPersistentEventString;
            }
            else
            {
                _currentBatchSize++;
                _dicJsondata.Add(data.UserId, data.UpdateCreditLimitPersistentEventString);
            }
            if (_currentBatchSize == MaxBatchSize || endOfBatch)
            {
                string message = BatchMessage(data, _dicJsondata);
                PublishMessageToKafkaTopic(_producer, message, _balancePersistentTopic, PartitionId);
                Reset();
            }
        }

        private void PublishMessageToKafkaTopic(IPublisher<ProducerData<Null, string>> producer, string message, string topic, int partition)
        {
            var producerData = new ProducerData<Null, string>(message, null, topic, partition);
            producer.Publish(producerData);
        }

        private string BatchMessage(UpdateCreditLimitPersistentRingEvent data, Dictionary<int, string> dicJsondata)
        {
            string valueMessage = data.Offset.ToString();
            foreach (var item in dicJsondata)
            {
                valueMessage += "|" + item.Value;
            }

            return valueMessage;
        }

        private void Reset()
        {
            this._currentBatchSize = 0;
            this._dicJsondata.Clear();
        }
    }
}
