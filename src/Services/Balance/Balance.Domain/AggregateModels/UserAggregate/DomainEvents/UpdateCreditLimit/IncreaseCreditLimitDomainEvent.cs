namespace ECom.Services.Balance.Domain.AggregateModels.UserAggregate.DomainEvents.UpdateCreditLimit
{
    public class IncreaseCreditLimitDomainEvent : BaseDomainEvent
    {
        public long Offset { get; set; }
        public int UserId { get; set; }
        public decimal CreditLimit { get; set; }
        public int SerializeHandlerId { get; set; }

        public IncreaseCreditLimitDomainEvent(long offset, int userId, decimal creditLimit, int serializeHandlerId)
        {
            Offset = offset;
            UserId = userId;
            CreditLimit = creditLimit;
            SerializeHandlerId = serializeHandlerId;
        }
    }
}
