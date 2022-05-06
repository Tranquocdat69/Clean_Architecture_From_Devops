namespace ECom.Services.Ordering.Persistent.RingBuffers.Events
#nullable disable
{
    public class PersistentEvent : BaseRingEvent
    {
        public string MessageData { get; set; }
        public Order Order { get; set; }
        public int HandlerId { get; set; }
        public long Offset { get; set; }
    }
}
