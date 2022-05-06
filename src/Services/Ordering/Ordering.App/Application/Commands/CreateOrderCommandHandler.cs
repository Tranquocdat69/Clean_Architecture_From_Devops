namespace FPTS.FIT.BDRD.Services.Ordering.App.Application.Commands
#nullable disable
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResponseData>
    {
        private readonly RingBuffer<CreateOrderRingEvent> _ring;
        private readonly IRequestManager<string> _requestManagement;
        private readonly IInMemoryOrderStore _orderStore;
        private readonly IMediator _mediator;
        private readonly string[] _keys = new[] { "catalog", "balance" };

        public CreateOrderCommandHandler(
            IMediator mediator,
            RingBuffer<CreateOrderRingEvent> ring,
            IRequestManager<string> requestManager, 
            IInMemoryOrderStore orderStore) 
        {
            _ring              = ring;
            _requestManagement = requestManager;
            _orderStore        = orderStore;
            _mediator          = mediator;
        }
        public async Task<ResponseData> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Tạo value object address
            var address           = new Address(request.Street, request.City);
            // Tạo Order
            var order             = new Order(request.UserId, address); 
            // Thêm item vào order
            foreach(var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
            }
            // Tạo IntegrationEvents request Id để gửi message qua 2 service là balance và catalog
            var requestId        = _requestManagement.GenernateRequestId();
            var catalogRequestId = requestId + " - " + _keys[0];
            var balanceRequestId = requestId + " - " + _keys[1];

            // Sử lý với ring
            HandleRing(order, balanceRequestId, catalogRequestId);

            // Chờ có phản hồi thử 2 service bằng request Id
            var rs = await WaitingResponseReceived(new[]
            {
                _requestManagement.GetResponseAsync(balanceRequestId),
                _requestManagement.GetResponseAsync(catalogRequestId)
            });

            var res = GetResponseData(rs.Cast<ResponseData>());

            await ResponseCheckingAsync(order, res);

            return res;
        }

        private void HandleRing (Order order, string balanceRequestId, string catalogRequestId)
        {
            // Lấy squence
            var sequence = _ring.Next();
            try
            {
                var data = _ring[sequence];
                data.Items = order.OrderItems.ToDictionary(x => x.ProductId, x => x.GetUnits());
                data.TotalCost = order.OrderItems.Sum(x => x.GetUnits() * x.GetUnitPrice());
                data.UserId = order.CustomerId;
                data.BalanceRequestId = balanceRequestId;
                data.CatalogRequestId = catalogRequestId;
            }
            finally
            {
                //Dẩy dữ liệu đi sử lý
                _ring.Publish(sequence);
            }

        }

        private ResponseData GetResponseData(IEnumerable<ResponseData> responses)
        {
            var response = new ResponseData();
            // Kiểm tra có kết quả phản hồi
            if (!responses.Contains(null) && responses.Any())
            {
                // Lấy ra ResponseData không thành công đầu tiên
                // Nếu không có thì lấy ResponseData đầu tiên
                response = responses.FirstOrDefault(x => !x.IsSuccess) ?? responses.FirstOrDefault();
                // Kiểm tra nếu response là không thành công và các response có ít nhất 1 thành công
                if (!response.IsSuccess && responses.Where(x => x.IsSuccess).Any())
                {
                    // Lấy topic 
                    response.Convension = response.RequestId.Contains(_keys[0]) ? _keys[1] : _keys[0];
                }

            }

            return response;
        }

        private async Task ResponseCheckingAsync(Order order, ResponseData response)
        {
            // Nếu sử lý ở 2 service thành công thì set order confirmed 
            // Nếu thất bại thì set order reject
            if (response.IsSuccess)
            {
                order.SetOrderConfirmed();
                _orderStore.Add(order.Id,order);
            }
            else
            {
                //var topic = balanceRequestId.Equals(res.RequestId) ? _commandTopic["Balance"] : _commandTopic["Catalog"];
                order.SetOrderRejected(response.Convension);
            }
            await _orderStore.DispatchDomainEvent(order);
        }

        private async Task<object[]> WaitingResponseReceived(Task<object>[] tasks)
        {
            return await Task.WhenAll(tasks);
        }
    }

    public class ResponseData
    {
        public string ReplyAddress { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = "Request timeout";
        public string Convension { get; set; }
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
