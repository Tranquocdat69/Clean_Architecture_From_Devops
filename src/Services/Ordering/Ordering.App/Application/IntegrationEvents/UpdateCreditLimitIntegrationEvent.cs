namespace ECom.Services.Ordering.App.Application.IntegrationEvents
#nullable disable
{
    public class UpdateCreditLimitIntegrationEvent : IIntegrationEvent
    {
        public UpdateCreditLimitIntegrationEvent(decimal totalCost, int userId, string replyAddress)
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
