namespace ECom.Services.Ordering.App.Application.Integrations
#nullable disable
{
    public class UpdateCreditLimitIntegration
    {
        public UpdateCreditLimitIntegration(decimal totalCost, int userId, string replyAddress)
        {
            TotalCost = totalCost;
            UserId = userId;
            ReplyAddress = replyAddress;
        }

        public decimal TotalCost { get;}
        public int UserId { get;}
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
