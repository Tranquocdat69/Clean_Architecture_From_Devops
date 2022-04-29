namespace ECom.Services.Ordering.Persistent.Models;
#nullable disable
public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public Address Address { get;  set; }
    public int CustomerId { get; set; }
    public List<OrderItem> OrderItems;
}
