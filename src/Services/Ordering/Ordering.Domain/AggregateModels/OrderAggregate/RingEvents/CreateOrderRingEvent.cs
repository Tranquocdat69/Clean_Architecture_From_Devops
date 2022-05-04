namespace ECom.Services.Ordering.Domain.AggregateModels.OrderAggregate.RingEvents
#nullable disable
{
    public class CreateOrderRingEvent : BaseRingEvent
    {
        public decimal TotalCost { get; set; }
        public Dictionary<int, int> Items { get; set; }
        public int UserId { get; set; }
        public string CatalogRequestId { get; set; }
        public string BalanceRequestId { get; set; }
        public Dictionary<string, string> JsonData { get; set; } = new();
    }   
}
