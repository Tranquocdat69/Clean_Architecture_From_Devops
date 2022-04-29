namespace ECom.Services.Ordering.App.Application.IntegrationEvents
#nullable disable
{
    public class UpdateProductAvaibleStockIntegrationEventsEvent : IIntegrationEvent
    {
        public UpdateProductAvaibleStockIntegrationEventsEvent(IDictionary<int, int> items, string replyAddress)
        {
            OrderItems = items;
            ReplyAddress = replyAddress;
        }

        public IDictionary<int, int> OrderItems { get;}
        public string ReplyAddress { get;}
    }
}
