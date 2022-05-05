namespace FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate
{
    public interface IInMemoryOrderStore : IKeyValuePairRepository<Order,string>
    {
        void Clear();
        Task DispatchDomainEvent(Order order);
        IEnumerable<OrderItem> GetItemsOfOrder(string id);
        Order GetOrder(string id);
        IEnumerable<Order> GetOrders();
        bool Remove(string orderId);
    }
}
