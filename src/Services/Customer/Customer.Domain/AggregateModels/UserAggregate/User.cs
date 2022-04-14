namespace ECom.Services.Customer.Domain.AggregateModels.UserAggregate
#nullable disable
{
    public class User : BaseEntity, IAggregateRoot
    {
        public User(int id, string name, decimal credit)
        {
            Id = id;
            Name = name;
            CreditLimit = credit;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal CreditLimit { get; private set; }

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
