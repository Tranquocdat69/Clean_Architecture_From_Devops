namespace ECom.Services.Balance.App.Application.Commands
{
    public class UpdateCreditLimitCommandHandler : IRequestHandler<UpdateCreditLimitCommand>
    {
        private readonly RingBuffer<UpdateCreditLimitRingEvent> _inputRing;

        public UpdateCreditLimitCommandHandler(RingBuffer<UpdateCreditLimitRingEvent> inputRing)
        {
            _inputRing = inputRing;
        }

        public Task<Unit> Handle(UpdateCreditLimitCommand request, CancellationToken cancellationToken)
        {
            var data = _inputRing[request.SequenceRing];
            data.UserId = request.UserId;
            data.TotalCost = request.TotalCost;
            data.ReplyAddress = request.ReplyAddress;

            return Task.FromResult(Unit.Value);
        }
    }
}
