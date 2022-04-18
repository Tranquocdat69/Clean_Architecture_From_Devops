namespace ECom.Services.Ordering.App.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, (string, bool)>
    {
        private readonly RingBuffer<CreateOrderEvent> _ring;
        private readonly InMemoryRequestManagement _requestManagement;

        public CreateOrderCommandHandler(RingBuffer<CreateOrderEvent> ring, InMemoryRequestManagement requestManagement) 
        {
            _ring              = ring;
            _requestManagement = requestManagement;
        }
        public async Task<(string, bool)> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address           = new Address(request.Street, request.City);
            var order             = new Order(request.UserId, address);
            var catalogRequestId  = _requestManagement.GenernateRequestId();
            var customerRequestId = _requestManagement.GenernateRequestId();
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
                data.CatalogRequestId  = catalogRequestId;
                data.CustomerRequestId = customerRequestId;
            }
            finally
            {
                _ring.Publish(sequence);
            }
            var catalogResponse = await _requestManagement.GetResponseAsync(catalogRequestId);
            var customerResponse = await _requestManagement.GetResponseAsync(customerRequestId);
            return new();
        }
    }
}
