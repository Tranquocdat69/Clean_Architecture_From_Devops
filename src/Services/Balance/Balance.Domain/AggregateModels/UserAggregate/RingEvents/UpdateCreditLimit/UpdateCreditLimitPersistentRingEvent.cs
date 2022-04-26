namespace ECom.Services.Balance.Domain.AggregateModels.UserAggregate.RingEvents.UpdateCreditLimit
{
    public class UpdateCreditLimitPersistentRingEvent : BaseRingEvent
    {
        [JsonIgnore]
        public long Offset { get; set; }
        public int UserId { get; set; }
        public decimal CreditLimit { get; set; }
        [JsonIgnore]
        public int SerializeHandlerId { get; set; }
        [JsonIgnore]
        public string UpdateCreditLimitPersistentEventString { get; set; }
    }
}
