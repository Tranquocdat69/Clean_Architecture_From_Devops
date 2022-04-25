<<<<<<< HEAD:src/Services/Balance/Balance.Domain/AggregateModels/UserAggregate/User.cs
﻿namespace FPTS.FIT.BDRD.Services.Balance.Domain.AggregateModels.UserAggregate
=======
﻿namespace ECom.Services.Balance.Domain.AggregateModels.UserAggregate
>>>>>>> bcad93d (change customer to balance service + validator behavior):src/Services/Customer/Customer.Domain/AggregateModels/UserAggregate/User.cs
#nullable disable
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public decimal CreditLimit { get; private set; }

        public User(string name, decimal creditLimit)
        {
            Name = name;
            CreditLimit = creditLimit;
        }

        public void DecreaseCash(decimal num)
        {
            this.CreditLimit -= num;
        }

        public void IncreaseCash(decimal num)
        {
            this.CreditLimit += num;
        }
    }
}
