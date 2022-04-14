namespace ECom.Services.Customer.Domain.AggregateModels.UserAggregate
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        void Update(User customer);
        User Get(int id);
    }
}
