namespace FPTS.FIT.BDRD.Services.Ordering.App.Application.DomainEventHandlers.OrderConfirmed
#nullable disable
{
    public class PersistanceWhenOrderConfirmedDomainEventHandler : IDomainEventHandler<OrderConfirmedDomainEvent>
    {
        private readonly IOrderRepository _repository;

        public PersistanceWhenOrderConfirmedDomainEventHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(OrderConfirmedDomainEvent notification, CancellationToken cancellationToken)
        {
            var order = notification.Order;
            _repository.Add(order);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
