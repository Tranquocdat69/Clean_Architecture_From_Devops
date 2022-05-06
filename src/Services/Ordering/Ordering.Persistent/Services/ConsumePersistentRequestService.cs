namespace ECom.Services.Ordering.Persistent.Services
{
    /// <summary>
    /// Task consume persistent message để bắt đầu các bước lưu vào db
    /// </summary>
    public class ConsumePersistentRequestService
    {
        private readonly IConsumerTask<Null, string> _consumerTask;
        private readonly string _topic;

        public ConsumePersistentRequestService(IConsumerTask<Null, string> consumerTask, string topic)
        {
            _consumerTask = consumerTask;
            _topic        = topic;
        }

        public void Execute()
        {
            var CurrentOffset = GetPersistentOffset() + 1;
            _consumerTask.StartConsumeMessage((record) =>
            {
                var arr = record.Message.Value.Split('|');
                long commandOffset = long.Parse(arr[0]);
                /*for (var i = 1; i < arr.Length; i++)
                {
                    var sequence = _persistentRing.Next();
                    var data = _persistentRing[sequence];
                    data.CommandOffset = commandOffset;
                    data.ReplicaOffset = replicaOffset;
                    data.ConvertHandlerId = currentConvertHandlerId;
                    data.CustomerBalanceString = arr[i];
                    _persistentRing.Publish(sequence);
                }
                currentConvertHandlerId++;
                if (currentConvertHandlerId > _numberOfConvertHandlers)
                {
                    currentConvertHandlerId = 1;
                }
                return record + _topic;*/
                return /*commandOffset*/0;
            }, _topic);
        }

        private long GetPersistentOffset()
        {
            return -1;
        }
    }
}
