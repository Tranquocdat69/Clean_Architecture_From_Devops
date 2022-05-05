namespace FPTS.FIT.BDRD.Services.Ordering.App.Application.Queries
{
    public record GetOrdersFromCustomerQuery(int CustomerId) : IRequest<List<OrderDTO>> { }
}
