namespace ECom.Services.Ordering.Domain.AggregateModels.OrderAggregate
#nullable disable
{
    public class Address : ValueObject
    {
        public String Street { get; private set; }
        public String City { get; private set; }
    }
}
