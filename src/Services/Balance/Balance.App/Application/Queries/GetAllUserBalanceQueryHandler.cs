using ECom.Services.Balance.App.DTOs;

namespace ECom.Services.Balance.App.Application.Queries
{
    public class GetAllUserBalanceQueryHandler : IRequestHandler<GetAllUserBalanceQuery, IEnumerable<UserDTO>>
    {
        public Task<IEnumerable<UserDTO>> Handle(GetAllUserBalanceQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<UserDTO> list = new List<UserDTO>();
            return Task.FromResult(list);
        }
    }
}
