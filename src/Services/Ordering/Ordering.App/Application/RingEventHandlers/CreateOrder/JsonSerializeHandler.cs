namespace FPTS.FIT.BDRD.Services.Ordering.App.Application.RingEventHandlers.CreateOrder
{
    public class JsonSerializeHandler : IRingHandler<CreateOrderRingEvent>
    {
        private readonly string _replyAddress;
        private readonly int _numberOfHandler;
        private readonly int _handlerId;
        private static Dictionary<int, List<int>> s_handlerManager = new();

        public JsonSerializeHandler(string replyAddress, int handlerId, int numberOfHandler)
        {
            _replyAddress    = replyAddress;
            _numberOfHandler = numberOfHandler;
            _handlerId       = handlerId;
            s_handlerManager.Add(handlerId, new List<int>());
        }
        public void OnEvent(CreateOrderRingEvent data, long sequence, bool endOfBatch)
        {
            if (IsMemberOfHandler(data.UserId))
            {
                var result = Task.WhenAll(
                    GetCatalogIntegrationEvent(data.Items),
                    GetBalanceIntegrationEvent(data.TotalCost, data.UserId)).Result;

                data.JsonData["catalog"] = result[0];
                data.JsonData["balance"] = result[1];
            }
        }

        private bool IsMemberOfHandler(int userId)
        {
            if (!s_handlerManager[_handlerId].Contains(userId))
            {
                int index = userId % _numberOfHandler;
                s_handlerManager[index].Add(userId);
            }
            return s_handlerManager[_handlerId].Contains(userId);
        }

        private Task<string> GetCatalogIntegrationEvent(Dictionary<int, int> items)
        {
            var IntegrationEvents = new UpdateProductAvaibleStockIntegrationEvent(
                items: items,
                replyAddress: _replyAddress
                );
            return Task.FromResult(JsonSerializer.Serialize(IntegrationEvents));
        }

        private Task<string> GetBalanceIntegrationEvent(decimal total, int userId)
        {

            var IntegrationEvents = new UpdateCreditLimitIntegrationEvent(
                totalCost: total,
                userId: userId,
                replyAddress: _replyAddress
                );
            return Task.FromResult(JsonSerializer.Serialize(IntegrationEvents));
        }
    }
}
