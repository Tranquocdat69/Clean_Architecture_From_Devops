namespace FPTS.FIT.BDRD.Services.Ordering.Infrastructure
#nullable disable
{
    public class InMemoryOrderStore : IInMemoryOrderStore
    {

        private static Dictionary<string, Order> s_dataStore;

        private readonly IMediator _mediator;

        public InMemoryOrderStore(IMediator mediator)
        {
            s_dataStore = s_dataStore ?? new();
            _mediator = mediator;
        }

        public IEnumerable<OrderItem> GetItemsOfOrder(string id)
        {
            return Get(id).OrderItems;
        }

        public IEnumerable<Order> GetOrders()
        {
            return s_dataStore.Select(x => x.Value);
        }

        public bool Remove(string orderId)
        {
            if (s_dataStore.ContainsKey(orderId))
            {
                s_dataStore.Remove(orderId);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            s_dataStore.Clear();
        }

        public void Add(string id, Order t)
        {
            /*order.Id = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            _mediator.DispatchDomainEventsAsync(order).Wait();*/
            s_dataStore.Add(id, t);
        }

        public bool Exist(string id)
        {
            return s_dataStore.ContainsKey(id);
        }

        public Order Get(string id)
        {
            return s_dataStore[id];
        }

        public async Task DispatchDomainEvent(Order order)
        {
            await _mediator.DispatchDomainEventsAsync(order);
        }
    }
}
