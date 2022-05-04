using ECom.Services.Balance.Domain.AggregateModels.UserAggregate;
using ECom.Services.Balance.Domain.AggregateModels.UserAggregate.DomainEvents.UpdateCreditLimit;
using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel.Extensions;

namespace ECom.Services.Balance.App.Application.RingEventHandlers.UpdateCreditLimit
{
    public class BusinessHandler : IRingHandler<UpdateCreditLimitRingEvent>
    {
        private readonly RingBuffer<UpdateCreditLimitReplyRingEvent> _ringRelplyBuffer;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        private readonly int _numberOfSerializeHandlers;
        private int _currentSerializeHandler = 1;

        public BusinessHandler(
            RingBuffer<UpdateCreditLimitReplyRingEvent> ringRelplyBuffer, 
            IUserRepository userRepository, 
            IMediator mediator,
            IConfiguration configuration)
        {
            _ringRelplyBuffer = ringRelplyBuffer;
            _userRepository = userRepository;
            _mediator = mediator;
            _numberOfSerializeHandlers = Int32.Parse(configuration.GetSection("Disruptor").GetSection("NumberOfSerializeHandlers").Value);
        }

        public void OnEvent(UpdateCreditLimitRingEvent data, long sequence, bool endOfBatch)
        {
            string message = "";
            bool isSuccess = false;

            var currentUser = _userRepository.GetT(data.UserId);
            if (currentUser is not null)
            {
                bool hasEnoughCreditLimit = currentUser.HasEnoughCreditLimit(data.TotalCost);
                if (hasEnoughCreditLimit)
                {
                    data.SerializeHandlerId = _currentSerializeHandler;
                    _currentSerializeHandler++;
                    if (_currentSerializeHandler > _numberOfSerializeHandlers)
                    {
                        _currentSerializeHandler = 1;
                    }

                    currentUser.DecreaseCash(data.TotalCost, data);
                    _mediator.DispatchDomainEventsAsync(currentUser);

                    isSuccess = true;
                    message = "Success";
                }
                else
                {
                    message = "Not enough credit limit";
                }
            }
            else
            {
                message = "User does not exist";
            }

            if (!data.IsCompensatedMessage)
            {
                long sq = 0L;
                sq = _ringRelplyBuffer.Next();
                var replyEvent = _ringRelplyBuffer[sq];
                replyEvent.IsSuccess = isSuccess;
                replyEvent.Message = message;
                replyEvent.UserId = data.UserId;
                replyEvent.ReplyAddress = data.ReplyAddress;
                replyEvent.RequestId = data.RequestId;
                _ringRelplyBuffer.Publish(sq);

            }
        }
    }
}
