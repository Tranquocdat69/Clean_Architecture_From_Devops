using ECom.BuildingBlocks.SharedKernel.Interfaces;

namespace ECom.BuildingBlocks.SharedKernel
#nullable disable
{
    public class BaseRingEvent : IRingData
    { 
        public string RequestId { get; private set; }
    }
}
