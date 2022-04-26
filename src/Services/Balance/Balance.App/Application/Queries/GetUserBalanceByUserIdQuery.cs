using ECom.Services.Balance.App.DTOs;

namespace ECom.Services.Balance.App.Application.Queries
{
    public class GetUserBalanceByUserIdQuery : IRequest<UserDTO>
    {
        public int UserId { get; set; }
    }
}
