<<<<<<< HEAD
﻿namespace FPTS.FIT.BDRD.Services.Balance.Domain.AggregateModels.UserAggregate
=======
﻿namespace ECom.Services.Balance.Domain.AggregateModels.UserAggregate
>>>>>>> bcad93d (change customer to balance service + validator behavior)
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        void Update(User Balance);
        User Get(int id);
    }
}
