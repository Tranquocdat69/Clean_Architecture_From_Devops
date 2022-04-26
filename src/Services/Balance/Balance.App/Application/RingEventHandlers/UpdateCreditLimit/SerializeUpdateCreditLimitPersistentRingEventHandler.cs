namespace ECom.Services.Balance.App.Application.RingEventHandlers.UpdateCreditLimit
{
    public class SerializeUpdateCreditLimitPersistentRingEventHandler : IRingHandler<UpdateCreditLimitPersistentRingEvent>
    {
        private readonly int _handlerId;
        private JsonSerializerOptions options;

        public SerializeUpdateCreditLimitPersistentRingEventHandler(int handlerId)
        {
            _handlerId = handlerId;
            options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }
        public void OnEvent(UpdateCreditLimitPersistentRingEvent data, long sequence, bool endOfBatch)
        {
            if (data.SerializeHandlerId == _handlerId)
            {
                data.UpdateCreditLimitPersistentEventString = JsonSerializer.Serialize(data, options);
            }
        }
    }
}
