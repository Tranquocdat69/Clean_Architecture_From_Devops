using System.Linq;
namespace FPTS.FIT.BDRD.Services.Ordering.Infrastructure
#nullable disable
{
    public class OrderRepository : IOrderRepository
    {
        private int _orderId { get; set; }

        private static List<Order> s_dataStore;

        private readonly IMediator _mediator;

        public OrderRepository(IMediator mediator)
        {
            s_dataStore = s_dataStore ?? new();
            _mediator   = mediator;
        }

        public Order GetOrder(string id)
        {
            return s_dataStore.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Order order)
        {
            order.Id = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            _mediator.DispatchDomainEventsAsync(order).Wait();
            s_dataStore.Add(order);
        }

        public IEnumerable<OrderItem> GetItemsOfOrder(string id)
        {
            return GetOrder(id).OrderItems;
        }

        public IEnumerable<Order> GetOrders()
        {
            return s_dataStore.AsReadOnly();
        }

        public bool Remove(string orderId)
        {
            var order = GetOrder(orderId);
            if (s_dataStore.Contains(order))
            {
                s_dataStore.Remove(order);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            s_dataStore.Clear();
        }
    }
}
