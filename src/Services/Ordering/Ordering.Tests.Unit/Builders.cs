namespace FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate;

public class AddressBuilder
{
    public Address Build()
    {
        return new Address("street", "city");
    }
}

public class OrderBuilder
{
    private readonly Order order;

    public OrderBuilder(Address address)
    {
        order = new Order(1,address);
    }

    public OrderBuilder AddOne(
        int productId,
        string productName,
        decimal unitPrice,
        decimal discount,
        string pictureUrl,
        int units = 1)
    {
        order.AddOrderItem(productId, productName, unitPrice, discount, pictureUrl, units);
        return this;
    }

    public Order Build()
    {
        return order;
    }
}
