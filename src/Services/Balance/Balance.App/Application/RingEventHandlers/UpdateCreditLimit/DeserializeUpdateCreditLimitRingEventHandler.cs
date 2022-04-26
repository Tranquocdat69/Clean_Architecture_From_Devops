using ECom.Services.Balance.App.Application.Commands;
using ECom.Services.Balance.Domain.AggregateModels.UserAggregate;

namespace ECom.Services.Balance.App.Application.RingEventHandlers.UpdateCreditLimit
{
    public class DeserializeUpdateCreditLimitRingEventHandler : IRingHandler<UpdateCreditLimitRingEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<DeserializeUpdateCreditLimitRingEventHandler> _logger;
        private readonly int _handlerId;
        private readonly IMediator _mediator;

        public DeserializeUpdateCreditLimitRingEventHandler(IUserRepository userRepository, ILogger<DeserializeUpdateCreditLimitRingEventHandler> logger, int handlerId, IMediator mediator)
        {
            _userRepository = userRepository;
            _logger = logger;
            _handlerId = handlerId;
            _mediator = mediator;
        }

        public void OnEvent(UpdateCreditLimitRingEvent data, long sequence, bool endOfBatch)
        {
            if (data.DeserializeHandlerId == _handlerId)
            {
                UpdateCreditLimitCommand updateCreditLimitCommand = DeserializeToUpdateCreditLimitCommand(data);
                _mediator.Send(updateCreditLimitCommand);

                _logger.LogInformation(data.ToString());
            }
        }

        private UpdateCreditLimitCommand DeserializeToUpdateCreditLimitCommand(UpdateCreditLimitRingEvent data)
        {
            UpdateCreditLimitCommand updateCreditLimitCommand = JsonSerializer.Deserialize<UpdateCreditLimitCommand>(data.UpdateCreditLimitCommandString);
            updateCreditLimitCommand.Offset = data.Offset;
            updateCreditLimitCommand.IsCompensatedMessage = data.IsCompensatedMessage;
            updateCreditLimitCommand.RequestId = data.RequestId;
            updateCreditLimitCommand.SequenceRing = data.SequenceRing;

            return updateCreditLimitCommand;
        }
    }
}
