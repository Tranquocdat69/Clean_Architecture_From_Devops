namespace ECom.Services.Ordering.App.Application.RingHandlers.CreateOrder
{
    public class BusinessHandler : IRingHandler<CreateOrderData>
    {
        public void OnEvent(CreateOrderData data, long sequence, bool endOfBatch)
        {
            throw new NotImplementedException();
        }
    }
}
