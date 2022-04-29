namespace ECom.Services.Ordering.Persistent.Models
#nullable disable
{
    public class OrderItem
    {
        public string ProductName;
        public string PictureUrl;
        public decimal UnitPrice;
        public decimal Discount;
        public int Units;
        public int ProductId { get; set; }
    }
}
