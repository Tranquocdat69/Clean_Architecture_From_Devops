using ECom.Services.Balance.App.DTOs;
using ECom.Services.Balance.Domain.AggregateModels.UserAggregate;

namespace ECom.Services.Balance.App.Application.Queries
{
    public class GetUserBalanceByUserIdQueryHandler : IRequestHandler<GetUserBalanceByUserIdQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;

        public GetUserBalanceByUserIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserDTO?> Handle(GetUserBalanceByUserIdQuery request, CancellationToken cancellationToken)
        {
            User user = _userRepository.GetT(request.UserId);
            UserDTO userDTO = null;
            if (user is not null)
            {
                userDTO = new UserDTO();
                userDTO.Id = user.Id;
                userDTO.Name = user.Name;
                userDTO.CreditLimit = user.CreditLimit;
            }
            return Task.FromResult(userDTO);
        }
    }
}
