namespace ECom.Services.Ordering.Infrastructure
#nullable disable
{
    public class OrderRepository : IOrderRepository
    {
        private int _orderId { get; set; }

        private static IDictionary<int, Order> s_dataStore;
        private readonly IMediator _mediator;

        public OrderRepository(IMediator mediator)
        {
            s_dataStore = s_dataStore ?? new Dictionary<int, Order>();
            _mediator   = mediator;
        }
        public void Add(int id, Order t)
        {
            _mediator.DispatchDomainEventsAsync(t).Wait();
            s_dataStore.Add(id, t);
        }

        public bool Exist(int id)
        {
            return s_dataStore.TryGetValue(id, out Order t);
        }

        public Order GetT(int id)
        {
            return s_dataStore[id];
        }

        public bool Add(Order t, int id)
        {
            throw new NotImplementedException();
        }
    }
}
