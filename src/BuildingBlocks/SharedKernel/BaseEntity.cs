using ECom.BuildingBlocks.SharedKernel.Interfaces;

namespace ECom.BuildingBlocks.SharedKernel
#nullable disable
{
    public abstract class BaseEntity
    {
        private List<BaseDomainEvent> _domainEvents;
        public IReadOnlyCollection<BaseDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(BaseDomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<BaseDomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(BaseDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
