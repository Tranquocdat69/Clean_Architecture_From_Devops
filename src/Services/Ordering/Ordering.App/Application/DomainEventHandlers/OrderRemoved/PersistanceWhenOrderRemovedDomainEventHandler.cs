namespace FPTS.FIT.BDRD.Services.Ordering.App.Application.DomainEventHandlers.OrderRemoved
{
    public class PersistanceWhenOrderRemovedDomainEventHandler : IDomainEventHandler<OrderRemovedDomainEvent>
    {
        private readonly IOrderRepository _repository;

        public PersistanceWhenOrderRemovedDomainEventHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(OrderRemovedDomainEvent notification, CancellationToken cancellationToken)
        {
            _repository.Delete(notification.Order);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
