<<<<<<< HEAD:src/Services/Balance/Balance.Infrastructure/CustomerRepository.cs
﻿namespace FPTS.FIT.BDRD.Services.Balance.Infrastructure
=======
﻿namespace ECom.Services.Balance.Infrastructure
>>>>>>> bcad93d (change customer to balance service + validator behavior):src/Services/Customer/Customer.Infrastructure/CustomerRepository.cs
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
