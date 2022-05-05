namespace FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate
{
    public interface IOrderRepository
    {
        void Clear();
        void Add(Order order);
        bool Remove(string orderId);
        Order GetOrder(string id);
        IEnumerable<OrderItem> GetItemsOfOrder(string id);
        IEnumerable<Order> GetOrders();
    }
}
