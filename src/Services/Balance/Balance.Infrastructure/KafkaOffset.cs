namespace ECom.Services.Balance.Infrastructure
{
    public class KafkaOffset
    {
        public int Id { get; set; }
        public long CommandOffset { get; set; } = -1;
        public long PersistentOffset { get; set; } = -1;
    }
}
