namespace ECom.Services.Ordering.App.Application.RingHandlers.CreateOrderEvent
{
    public class LogHandler : IRingHandler<CreateOrderData>
    {
        public void OnEvent(CreateOrderData data, long sequence, bool endOfBatch)
        {
            throw new NotImplementedException();
        }
    }
}
