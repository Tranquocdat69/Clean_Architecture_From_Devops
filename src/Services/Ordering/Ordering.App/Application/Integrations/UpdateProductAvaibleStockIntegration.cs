namespace ECom.Services.Ordering.App.Application.Integrations
#nullable disable
{
    public class UpdateProductAvaibleStockIntegration
    {
        public UpdateProductAvaibleStockIntegration(IEnumerable<int> productIds, string replyAddress)
        {
            ProductIds = productIds;
            ReplyAddress = replyAddress;
        }

        public IEnumerable<int> ProductIds { get;}
        public string ReplyAddress { get;}

        /// <summary>
        /// Convert object hiện tại sang Json string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string ids = string.Join(",", ProductIds);
            return "{\"ProductIds\":["+ids+"],\"ReplyAddress\":\"\"}";
        }
    }
}
