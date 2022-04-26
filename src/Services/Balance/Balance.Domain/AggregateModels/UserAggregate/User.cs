
using ECom.Services.Balance.Domain.AggregateModels.UserAggregate.DomainEvents.UpdateCreditLimit;
using ECom.Services.Balance.Domain.AggregateModels.UserAggregate.RingEvents.UpdateCreditLimit;

namespace ECom.Services.Balance.Domain.AggregateModels.UserAggregate
#nullable disable
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public decimal CreditLimit { get; private set; }

        public User(int id ,string name, decimal creditLimit)
        {
            Id = id;
            Name = name;
            CreditLimit = creditLimit;
        }
        
        public User(string name, decimal creditLimit)
        {
            Name = name;
            CreditLimit = creditLimit;
        }

        public void DecreaseCash(decimal num, UpdateCreditLimitRingEvent ringEventData)
        {
            this.CreditLimit -= num;
            var @event = new DecreaseCreditLimitDomainEvent(
                        offset: ringEventData.Offset,
                        userId: ringEventData.UserId,
                        creditLimit: this.CreditLimit,
                        serializeHandlerId: ringEventData.SerializeHandlerId);
            AddDomainEvent(@event);
        }

        public void IncreaseCash(decimal num, UpdateCreditLimitRingEvent ringEventData)
        {
            this.CreditLimit += num;
            var @event = new IncreaseCreditLimitDomainEvent(
                        offset: ringEventData.Offset,
                        userId: ringEventData.UserId,
                        creditLimit: this.CreditLimit,
                        serializeHandlerId: ringEventData.SerializeHandlerId);
            AddDomainEvent(@event);
        }

        public bool HasEnoughCreditLimit(decimal totalCost)
        {
            return (this.CreditLimit - totalCost) >= 0;
        }
    }
}
