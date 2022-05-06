namespace ECom.Services.Ordering.Domain.AggregateModels.OrderAggregate
{
    public interface IOrderRepository : IKeyValuePairRepository<Order, int>
    {
    }
}
