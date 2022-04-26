using ECom.Services.Balance.Domain.AggregateModels.UserAggregate;
using ECom.Services.Balance.Domain.AggregateModels.UserAggregate.DomainEvents;

namespace ECom.Services.Balance.App.Application.RingEventHandlers.UpdateCreditLimit
{
    public class UpdateCreditLimitRingEventHandler : IRingHandler<UpdateCreditLimitRingEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        private readonly int _numberOfSerializeHandlers;
        private int _currentSerializeHandlerId = 0;

        public UpdateCreditLimitRingEventHandler(
            IUserRepository userRepository,
            IMediator mediator,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mediator = mediator;
            _numberOfSerializeHandlers = Int32.Parse(configuration.GetSection("Disruptor").GetSection("NumberOfSerializeHandlers").Value);
        }

        public void OnEvent(UpdateCreditLimitRingEvent data, long sequence, bool endOfBatch)
        {
            string message = "User does not exist";
            bool isSuccess = false;

            var currentUser = _userRepository.GetT(data.UserId);
            if (currentUser is not null)
            {
                data.SerializeHandlerId = GetCurrentSerializeHandlerId(ref this._currentSerializeHandlerId, _numberOfSerializeHandlers);

                if (data.IsCompensatedMessage)
                {
                    IncreaseCreditLimit(currentUser, data);
                }
                else
                {
                    DecreaseCreditLimit(currentUser, data, ref isSuccess, ref message);
                }
                _mediator.DispatchDomainEventsAsync(currentUser);
            }
            else
            {
                DispatchReplyDomainEvent(data, isSuccess, message);
            }
        }

        private void IncreaseCreditLimit(User currentUser, UpdateCreditLimitRingEvent data)
        {
            currentUser.IncreaseCash(data.TotalCost, data);
        }

        private void DecreaseCreditLimit(User currentUser, UpdateCreditLimitRingEvent data, ref bool isSuccess, ref string message)
        {
            bool hasEnoughCreditLimit = currentUser.HasEnoughCreditLimit(data.TotalCost);
            if (hasEnoughCreditLimit)
            {
                currentUser.DecreaseCash(data.TotalCost, data);
                isSuccess = true;
                message = "Success";
            }
            else
            {
                message = "Not enough credit limit";
            }
            DispatchReplyDomainEvent(data, isSuccess, message);
        }

        private int GetCurrentSerializeHandlerId(ref int currentSerializeHandlerId, int numberOfSerializeHandlers)
        {
            currentSerializeHandlerId++;
            if (currentSerializeHandlerId > numberOfSerializeHandlers)
            {
                currentSerializeHandlerId = 1;
            }

            return currentSerializeHandlerId;
        }

        private void DispatchReplyDomainEvent(UpdateCreditLimitRingEvent data, bool isSuccess, string message)
        {
            var @event = new ReplyDomainEvent(data.ReplyAddress, isSuccess, message, data.UserId, data.RequestId);
            _mediator.Publish(@event);
        }
    }
}
