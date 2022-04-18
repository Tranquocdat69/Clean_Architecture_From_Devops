﻿namespace ECom.Services.Customer.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly CustomerDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public UserRepository(CustomerDbContext context, ILogger<UserRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public void Update(User customer)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
