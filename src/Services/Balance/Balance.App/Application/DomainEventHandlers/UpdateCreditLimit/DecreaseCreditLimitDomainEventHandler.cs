using ECom.Services.Balance.Domain.AggregateModels.UserAggregate.DomainEvents.UpdateCreditLimit;

namespace ECom.Services.Balance.App.Application.DomainEventHandlers.UpdateCreditLimit
{
    public class DecreaseCreditLimitDomainEventHandler : IDomainEventHandler<DecreaseCreditLimitDomainEvent>
    {
        private readonly RingBuffer<UpdateCreditLimitPersistentRingEvent> _ringPersistentBuffer;

        public DecreaseCreditLimitDomainEventHandler(
            RingBuffer<UpdateCreditLimitPersistentRingEvent> ringPersistentBuffer,
            IConfiguration configuration)
        {
            _ringPersistentBuffer = ringPersistentBuffer;
        }

        public Task Handle(DecreaseCreditLimitDomainEvent @event, CancellationToken cancellationToken)
        {
            long sq = 0L;
            sq = _ringPersistentBuffer.Next();
            var persistentEvent = _ringPersistentBuffer[sq];
            persistentEvent.UserId = @event.UserId;
            persistentEvent.CreditLimit = @event.CreditLimit;
            persistentEvent.Offset = @event.Offset;
            persistentEvent.SerializeHandlerId = @event.SerializeHandlerId;
          
            _ringPersistentBuffer.Publish(sq);

            return Task.CompletedTask;
        }

    }
}
