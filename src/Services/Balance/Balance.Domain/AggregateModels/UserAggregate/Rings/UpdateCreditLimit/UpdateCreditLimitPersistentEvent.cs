<<<<<<< HEAD
<<<<<<< HEAD
﻿namespace FPTS.FIT.BDRD.Services.Balance.Domain.AggregateModels.UserAggregate.Rings.UpdateCreditLimit
=======
﻿namespace ECom.Services.Balance.Domain.AggregateModels.UserAggregate.Rings.UpdateCreditLimit
>>>>>>> bcad93d (change customer to balance service + validator behavior)
=======
﻿using System.Text.Json.Serialization;

namespace ECom.Services.Balance.Domain.AggregateModels.UserAggregate.Rings.UpdateCreditLimit
>>>>>>> f426eda (event domain)
{
    public class UpdateCreditLimitPersistentEvent : BaseRingEvent
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
