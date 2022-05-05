namespace FPTS.FIT.BDRD.Services.Ordering.App.Application.Queries
{
    public class GetOrdersFromCustomerQueryHandler : IRequestHandler<GetOrdersFromCustomerQuery, List<OrderDTO>>
    {
        private readonly IInMemoryOrderStore _orderStore;

        public GetOrdersFromCustomerQueryHandler(IInMemoryOrderStore orderStore)
        {
            _orderStore = orderStore;
        }
        public Task<List<OrderDTO>> Handle(GetOrdersFromCustomerQuery request, CancellationToken cancellationToken)
        {
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            var order = _orderStore.GetOrders().Where(x => x.CustomerId == request.CustomerId);
            foreach(var item in order)
            {
                orderDTOs.Add(new OrderDTO(item.Address.City, item.Address.Street, item.OrderDate, item.OrderItems.Count()));
            }

            return Task.FromResult(orderDTOs);
        }
    }
}
