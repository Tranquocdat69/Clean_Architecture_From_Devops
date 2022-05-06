namespace ECom.Services.Ordering.Persistent
{
    public class KafkaOffset
    {
        public int Id { get; set; }
        public long PersistentOffset { get; set; }
    }
}
