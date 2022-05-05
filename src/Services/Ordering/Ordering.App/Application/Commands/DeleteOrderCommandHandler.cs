namespace FPTS.FIT.BDRD.Services.Ordering.App.Application.Commands
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IInMemoryOrderStore _orderStore;

        public DeleteOrderCommandHandler(IInMemoryOrderStore orderStore)
        {
            _orderStore = orderStore;
        }
        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _orderStore.GetOrder(request.OrderId);
            order.SetOrderRemoved();
            await _orderStore.DispatchDomainEvent(order);
            var result = _orderStore.Remove(request.OrderId);
            return result;
        }
    }
}
