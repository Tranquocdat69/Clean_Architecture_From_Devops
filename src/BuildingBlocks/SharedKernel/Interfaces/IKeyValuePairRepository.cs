namespace ECom.BuildingBlocks.SharedKernel.Interfaces
{
    public interface IKeyValuePairRepository<T, TId> where T : class, IAggregateRoot
    {
        bool Add(T t, TId id);
        bool Exist(TId id);
        T GetT(TId id);
        IUnitOfWork UnitOfWork { get; }
    }
}
