namespace ECom.Services.Customer.App.Application.RingHandlers.UpdateCreditLimit
{
    public class PersistentHandler : IRingHandler<UpdateCreditLimitPersistentEvent>
    {
        public void OnEvent(UpdateCreditLimitPersistentEvent data, long sequence, bool endOfBatch)
        {
            throw new NotImplementedException();
        }
    }
}
