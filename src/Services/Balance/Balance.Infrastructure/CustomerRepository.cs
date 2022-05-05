namespace FPTS.FIT.BDRD.Services.Balance.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly BalanceDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public UserRepository(BalanceDbContext context, ILogger<UserRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public void Update(User Balance)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
