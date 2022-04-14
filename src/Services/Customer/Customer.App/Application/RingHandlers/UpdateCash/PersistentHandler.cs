namespace ECom.Services.Customer.App.Application.RingHandlers.UpdateCash
{
    public class PersistentHandler : IRingHandler<UpdateCashPersistentEvent>
    {
        public void OnEvent(UpdateCashPersistentEvent data, long sequence, bool endOfBatch)
        {
            throw new NotImplementedException();
        }
    }
}
