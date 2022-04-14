using Disruptor;
namespace ECom.BuildingBlocks.SharedKernel.Interfaces
{
    public interface IRingHandler<T> : IEventHandler<T> where T : class, IRingData
    {
    }
}
