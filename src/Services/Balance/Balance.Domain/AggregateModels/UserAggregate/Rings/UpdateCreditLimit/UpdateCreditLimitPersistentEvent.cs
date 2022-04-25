<<<<<<< HEAD
﻿namespace FPTS.FIT.BDRD.Services.Balance.Domain.AggregateModels.UserAggregate.Rings.UpdateCreditLimit
=======
﻿namespace ECom.Services.Balance.Domain.AggregateModels.UserAggregate.Rings.UpdateCreditLimit
>>>>>>> bcad93d (change customer to balance service + validator behavior)
{
    public class UpdateCreditLimitPersistentEvent : BaseRingEvent
    {
        public long Offset { get; set; }
        public string UserId { get; set; }
        public double Balance { get; set; }
    }
}
