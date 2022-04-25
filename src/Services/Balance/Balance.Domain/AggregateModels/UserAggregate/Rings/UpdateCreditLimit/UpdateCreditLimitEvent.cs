<<<<<<< HEAD
﻿namespace FPTS.FIT.BDRD.Services.Balance.Domain.AggregateModels.UserAggregate.Rings.UpdateCreditLimit
=======
﻿namespace ECom.Services.Balance.Domain.AggregateModels.UserAggregate.Rings.UpdateCreditLimit
>>>>>>> bcad93d (change customer to balance service + validator behavior)
{
    public class UpdateCreditLimitEvent : BaseRingEvent
    {
        public int UserId { get; set; }
        public decimal TotalCost { get; set; }
        public string ReplyAddress { get; set; }
        public long Offset { get; set; }
        public bool IsCompensatedMessage { get; set; }
    }
}
