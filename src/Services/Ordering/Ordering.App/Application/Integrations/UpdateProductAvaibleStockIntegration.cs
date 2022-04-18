namespace ECom.Services.Ordering.App.Application.Integrations
#nullable disable
{
    public class UpdateProductAvaibleStockIntegration
    {
        public UpdateProductAvaibleStockIntegration(IEnumerable<int> productIds, string requestId, string replyAddress)
        {
            ProductIds = productIds;
            RequestId = requestId;
            ReplyAddress = replyAddress;
        }

        public IEnumerable<int> ProductIds { get;}
        public string RequestId { get;}
        public string ReplyAddress { get;}

        /// <summary>
        /// Convert object hiện tại sang Json string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "";
        }
    }
}
