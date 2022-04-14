namespace ECom.Services.Ordering.App.Application.RingHandlers.CreateOrderEvent
{
    public class CustomerIntegrationHandler : IRingHandler<CreateOrderData>
    {
        public void OnEvent(CreateOrderData data, long sequence, bool endOfBatch)
        {
            throw new NotImplementedException();
        }
    }
}
