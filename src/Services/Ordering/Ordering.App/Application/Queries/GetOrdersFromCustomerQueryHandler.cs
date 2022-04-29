namespace ECom.Services.Ordering.App.Application.Queries
{
    public class GetOrdersFromCustomerQueryHandler : IRequestHandler<GetOrdersFromCustomerQuery, List<OrderDTO>>
    {
        private readonly IOrderRepository _repository;

        public GetOrdersFromCustomerQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        public Task<List<OrderDTO>> Handle(GetOrdersFromCustomerQuery request, CancellationToken cancellationToken)
        {
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            var order = _repository.GetOrders().Where(x => x.CustomerId == request.CustomerId);
            foreach(var item in order)
            {
                orderDTOs.Add(new OrderDTO(item.Address.City, item.Address.Street, item.OrderDate, item.OrderItems.Count()));
            }

            return Task.FromResult(orderDTOs);
        }
    }
}
