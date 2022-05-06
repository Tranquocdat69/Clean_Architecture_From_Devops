namespace ECom.Services.Ordering.Persistent.RingBuffers.EventHandlers.Persistent
{
    public class DeserializeMessageDataHandler : IRingHandler<PersistentEvent>
    {
        private readonly int _handlerId;

        public DeserializeMessageDataHandler(int handlerId)
        {
            _handlerId = handlerId;
        }
        public void OnEvent(PersistentEvent data, long sequence, bool endOfBatch)
        {
            if(_handlerId == data.HandlerId)
            {
                data.Order = JsonSerializer.Deserialize<Order>(data.MessageData);
            }
        }
    }
}
