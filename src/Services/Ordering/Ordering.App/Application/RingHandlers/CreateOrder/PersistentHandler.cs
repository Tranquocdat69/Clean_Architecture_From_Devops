namespace ECom.Services.Ordering.App.Application.RingHandlers.CreateOrderEvent
{
    public class PersistentHandler : IRingHandler<CreateOrderData>
    {
        public void OnEvent(CreateOrderData data, long sequence, bool endOfBatch)
        {
            throw new NotImplementedException();
        }
    }
}
