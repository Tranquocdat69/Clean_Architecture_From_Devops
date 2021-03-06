using ECom.Services.Balance.Domain.AggregateModels.UserAggregate;
using NetMQ;
using NetMQ.Sockets;

namespace ECom.Services.Balance.App.Application.RingEventHandlers.UpdateCreditLimit
{
    public class UpdateCreditLimitReplyRingEventHandler : IRingHandler<UpdateCreditLimitReplyRingEvent>
    {
        private IDictionary<string, PushSocket> _dicPushSocket;
        private readonly IUserRepository _userRepository;
        private PushSocket socket;

        public UpdateCreditLimitReplyRingEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _dicPushSocket = _dicPushSocket ?? new Dictionary<string, PushSocket>();
        }

        public void OnEvent(UpdateCreditLimitReplyRingEvent data, long sequence, bool endOfBatch)
        {
            if (_dicPushSocket.ContainsKey(data.ReplyAddress))
            {
                socket = _dicPushSocket[data.ReplyAddress];
            }
            else
            {
                socket = new PushSocket();
                string host = "tcp://" + data.ReplyAddress;
                socket.Connect(host);
                _dicPushSocket.Add(data.ReplyAddress, socket);
            }

            socket.SendFrame(data.ToString());
        }
    }
}
