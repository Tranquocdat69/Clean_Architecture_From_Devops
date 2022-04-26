using ECom.Services.Balance.Domain.AggregateModels.UserAggregate.DomainEvents;

namespace ECom.Services.Balance.App.Application.DomainEventHandlers
{
    public class ReplyDomainEventHandler : IDomainEventHandler<ReplyDomainEvent>
    {
        private readonly RingBuffer<UpdateCreditLimitReplyRingEvent> _ringRelplyBuffer;

        public ReplyDomainEventHandler(RingBuffer<UpdateCreditLimitReplyRingEvent> ringRelplyBuffer)
        {
            _ringRelplyBuffer = ringRelplyBuffer;
        }

        public Task Handle(ReplyDomainEvent @event, CancellationToken cancellationToken)
        {
            long sq = 0L;
            sq = _ringRelplyBuffer.Next();
            var replyEvent = _ringRelplyBuffer[sq];
            replyEvent.IsSuccess = @event.IsSuccess;
            replyEvent.Message = @event.Message;
            replyEvent.UserId = @event.UserId;
            replyEvent.ReplyAddress = @event.ReplyAddress;
            replyEvent.RequestId = @event.RequestId;
            _ringRelplyBuffer.Publish(sq);

            return Task.CompletedTask;
        }
    }
}
