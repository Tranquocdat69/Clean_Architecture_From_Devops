namespace ECom.Services.Ordering.App.Application.RingHandlers.CreateOrder
{
    public class PersistentHandler : IRingHandler<CreateOrderEvent>
    {
        public void OnEvent(CreateOrderEvent data, long sequence, bool endOfBatch)
        {
            throw new NotImplementedException();
        }
    }
}
