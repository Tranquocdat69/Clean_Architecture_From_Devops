namespace ECom.Services.Ordering.App.Application.Queries
{
    public class GetOrdersFromCustomerQueryHandler : IRequestHandler<GetOrdersFromCustomerQuery, OrderDTO>
    {
        public Task<OrderDTO> Handle(GetOrdersFromCustomerQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
