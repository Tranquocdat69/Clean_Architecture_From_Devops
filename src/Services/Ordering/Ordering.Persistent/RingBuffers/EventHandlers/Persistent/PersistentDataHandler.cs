namespace ECom.Services.Ordering.Persistent.RingBuffers.EventHandlers.Persistent
{
    public class PersistentDataHandler : IRingHandler<PersistentEvent>
    {
        private const int c_maxBatch = 20;
        private int _currentBatchSize = 0;
        private KafkaOffset _kafkaOffset = new(){Id = 1};
        private Dictionary<int, Order> _orderBatch = new();
        public void OnEvent(PersistentEvent data, long sequence, bool endOfBatch)
        {
            var key = data.Order.CustomerId;
            if (_orderBatch.ContainsKey(key))
            {
                _orderBatch[key] = data.Order;
            }
            else
            {
                _currentBatchSize++;
                _orderBatch.Add(key, data.Order);
            }

            if(endOfBatch || _currentBatchSize == c_maxBatch)
            {
                _currentBatchSize = 0;
                foreach(var order in _orderBatch)
                {
                     
                }
            }
        }
    }
}
