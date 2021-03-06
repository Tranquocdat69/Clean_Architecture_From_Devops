namespace ECom.Services.Balance.Domain.AggregateModels.UserAggregate.RingEvents.UpdateCreditLimit
{
    public class UpdateCreditLimitRingEvent : BaseRingEvent
    {
        public int UserId { get; set; }
        public decimal TotalCost { get; set; }
        public string ReplyAddress { get; set; }
        public long Offset { get; set; }
        public bool IsCompensatedMessage { get; set; }
        public long SequenceRing { get; set; }
        public string UpdateCreditLimitCommandString { get; set; }
        public int SerializeHandlerId { get; set; }
        public int DeserializeHandlerId { get; set; }
    }
}
