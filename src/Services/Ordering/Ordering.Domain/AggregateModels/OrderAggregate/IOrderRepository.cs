namespace ECom.Services.Ordering.Domain.AggregateModels.OrderAggregate
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        void Add(Order order);
        Order GetOrder(string id);
        IEnumerable<OrderItem> GetItemsOfOrder(string id);
        IEnumerable<Order> GetOrders();
    }
}
