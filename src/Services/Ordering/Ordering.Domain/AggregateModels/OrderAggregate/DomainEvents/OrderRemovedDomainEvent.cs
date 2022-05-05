namespace FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate.DomainEvents
{
    public class OrderRemovedDomainEvent : BaseDomainEvent
    {
        public Order Order { get; private set; }

        public OrderRemovedDomainEvent(Order order)
        {
            Order = order;
        }
    }
}
