namespace ECom.Services.Ordering.Domain.AggregateModels.OrderAggregate;
#nullable disable
public class Order : BaseEntity, IAggregateRoot
{
    public DateTime OrderDate { get; set; }
    public Address Address { get; private set; }
    public int CustomerId { get; set; }
    private readonly List<OrderItem> _orderItems;
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public Order(int cusId, Address add)
    {
        CustomerId = cusId;
        Address = add;
        OrderDate = DateTime.Now;

        AddOrderCreatedEvent();
    }

    public Order()
    {
        _orderItems = new List<OrderItem>();
    }

    // DDD Patterns comment
    // This Order AggregateRoot's method "AddOrderitem()" should be the only way to add Items to the Order,
    // so any behavior (discounts, etc.) and validations are controlled by the AggregateRoot 
    // in order to maintain consistency between the whole Aggregate. 
    public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
    {
        var existingOrderForProduct = _orderItems.Where(o => o.ProductId == productId)
            .SingleOrDefault();

        if (existingOrderForProduct != null)
        {
            //if previous line exist modify it with higher discount  and units..

            if (discount > existingOrderForProduct.GetCurrentDiscount())
            {
                existingOrderForProduct.SetNewDiscount(discount);
            }

            existingOrderForProduct.AddUnits(units);
        }
        else
        {
            //add validated new order item

            var orderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);
            _orderItems.Add(orderItem);
        }
    }

    private void AddOrderCreatedEvent()
    {
        throw new NotImplementedException();
    }
}
