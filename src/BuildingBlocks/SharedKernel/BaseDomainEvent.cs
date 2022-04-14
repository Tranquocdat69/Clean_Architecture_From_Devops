using MediatR;

namespace ECom.BuildingBlocks.SharedKernel
{
    public abstract class BaseDomainEvent : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.Now;
    }
}