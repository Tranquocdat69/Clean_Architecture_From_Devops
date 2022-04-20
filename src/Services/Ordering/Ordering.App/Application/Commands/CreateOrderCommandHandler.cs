namespace ECom.Services.Ordering.App.Application.Commands
#nullable disable
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResponseData>
    {
        private readonly RingBuffer<CreateOrderEvent> _ring;
        private readonly InMemoryRequestManagement _requestManagement;

        public CreateOrderCommandHandler(RingBuffer<CreateOrderEvent> ring, InMemoryRequestManagement requestManagement) 
        {
            _ring              = ring;
            _requestManagement = requestManagement;
        }
        public async Task<ResponseData> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address           = new Address(request.Street, request.City);
            var order             = new Order(request.UserId, address);
            var catalogRequestId = _requestManagement.GenernateRequestId();
            var balanceRequestId = _requestManagement.GenernateRequestId();
            foreach(var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
            }
            var sequence = _ring.Next();
            try
            {
                var data               = _ring[sequence];
                data.OrderItems        = order.OrderItems;
                data.Address           = address;
                data.UserId            = request.UserId;
                data.BalanceRequestId = balanceRequestId;
                data.CatalogRequestId = catalogRequestId;
            }
            finally
            {
                _ring.Publish(sequence);
            }

            var tasks = await Task.WhenAll(
                _requestManagement.GetResponseAsync(balanceRequestId),
                _requestManagement.GetResponseAsync(catalogRequestId));
            return GetRequestResult(tasks);
        }

        private ResponseData GetRequestResult(object[] objs)
        {
            var response = new ResponseData();
            if (!objs.Contains(null) && objs.Any())
            {
                var resList = objs.Cast<ResponseData>();
                response = resList.FirstOrDefault(x => !x.IsSuccess) ?? resList.FirstOrDefault();
            }
            return response;
        }
    }

    public class ResponseData
    {
        public string ReplyAddress { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = "Request timeout";
        public string RequestId { get; set; }

        public override string ToString()
        {
            return "{\"IsSuccess\":" + IsSuccess + ",\"Message\":\"" + Message
            + "\",\"ReplyAddress\":\"" + ReplyAddress + "\",\"RequestId\":\"" + RequestId + "\"}";
        }

        public static ResponseData FromString(string str)
        {
            var splits = Regex.Replace(str, "[}{\"]", string.Empty).Split(',');
            return new ResponseData
            {
                IsSuccess = bool.Parse(splits[0].Substring(10)),
                Message = splits[1].Substring(8),
                ReplyAddress = splits[2].Substring(13),
                RequestId = splits[3].Substring(10)
            };
        }
    }
}
