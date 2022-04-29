namespace ECom.Services.Ordering.App.Application.IntegrationEvents
#nullable disable
{
    public class UpdateCreditLimitIntegrationEventsEvent : IIntegrationEvent
    {
        public UpdateCreditLimitIntegrationEventsEvent(decimal totalCost, int userId, string replyAddress)
        {
            TotalCost = totalCost;
            UserId = userId;
            ReplyAddress = replyAddress;
        }

        public decimal TotalCost { get;}
        public int UserId { get;}
        public string ReplyAddress { get;}
    }
}
