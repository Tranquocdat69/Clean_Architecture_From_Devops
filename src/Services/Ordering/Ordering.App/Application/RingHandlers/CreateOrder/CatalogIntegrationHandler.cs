namespace ECom.Services.Ordering.App.Application.RingHandlers.CreateOrderEvent
{
    public class CatalogIntegrationHandler : IRingHandler<CreateOrderData>
    {
        public CatalogIntegrationHandler()
        {

        }
        public void OnEvent(CreateOrderData data, long sequence, bool endOfBatch)
        {
            throw new NotImplementedException();
        }
    }
}
