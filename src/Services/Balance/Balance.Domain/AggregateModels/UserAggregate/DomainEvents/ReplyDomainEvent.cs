namespace ECom.Services.Balance.Domain.AggregateModels.UserAggregate.DomainEvents
{
    public class ReplyDomainEvent : BaseDomainEvent
    {
        public string ReplyAddress { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public string RequestId { get; set; }

        public ReplyDomainEvent(string replyAddress, bool isSuccess, string message, int userId, string requestId)
        {
            ReplyAddress = replyAddress;
            IsSuccess = isSuccess;
            Message = message;
            UserId = userId;
            RequestId = requestId;
        }
    }
}
