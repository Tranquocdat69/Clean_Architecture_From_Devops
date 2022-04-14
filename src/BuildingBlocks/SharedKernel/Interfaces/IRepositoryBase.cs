namespace ECom.BuildingBlocks.SharedKernel.Interfaces
{
    public interface IRepositoryBase<T> where T : class, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
