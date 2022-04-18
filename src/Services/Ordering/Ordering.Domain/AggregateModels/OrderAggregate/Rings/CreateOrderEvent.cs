namespace ECom.Services.Ordering.Domain.AggregateModels.OrderAggregate.Rings
#nullable disable
{
    public class CreateOrderEvent : BaseRingEvent
    {
        public Address Address { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public int UserId { get; set; }
        public string CatalogRequestId { get; set; }
        public string CustomerRequestId { get; set; }
    }
}
