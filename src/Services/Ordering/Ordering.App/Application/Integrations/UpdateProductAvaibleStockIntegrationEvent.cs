namespace ECom.Services.Ordering.App.Application.Integrations
#nullable disable
{
    public class UpdateProductAvaibleStockIntegrationEvent : IIntegration
    {
        public UpdateProductAvaibleStockIntegrationEvent(IDictionary<int, int> items, string replyAddress)
        {
            OrderItems = items;
            ReplyAddress = replyAddress;
        }

        public IDictionary<int, int> OrderItems { get;}
        public string ReplyAddress { get;}
    }
}
