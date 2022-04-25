<<<<<<< HEAD
﻿namespace FPTS.FIT.BDRD.Services.Balance.Domain.AggregateModels.UserAggregate.Rings.UpdateCreditLimit
=======
﻿namespace ECom.Services.Balance.Domain.AggregateModels.UserAggregate.Rings.UpdateCreditLimit
>>>>>>> bcad93d (change customer to balance service + validator behavior)
{
    public class UpdateCreditLimitEvent : BaseRingEvent
    {
        public long Offset { get; set; }
        public string UserId { get; set; }
        public double Amount { get; set; }
        public string ReplyAddress { get; set; }
        public string UpdateCreditLimitCommandString { get; set; }
    }
}
