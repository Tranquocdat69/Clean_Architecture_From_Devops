namespace ECom.Services.Customer.App.Application.RingHandlers.UpdateCash
{
    public class LogHandler : IRingHandler<UpdateCashEvent>
    {
        public void OnEvent(UpdateCashEvent data, long sequence, bool endOfBatch)
        {
            throw new NotImplementedException();
        }
    }
}
