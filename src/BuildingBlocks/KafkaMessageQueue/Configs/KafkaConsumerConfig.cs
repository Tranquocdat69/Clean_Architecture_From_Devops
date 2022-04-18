namespace ECom.BuildingBlocks.MessageQueue.KafkaMessageQueue.Configs
{
    public class KafkaConsumerConfig
    {
        public string GroupId { get; set; } = "groupid";
        public string BootstrapServers { get; set; } = "localhost:9092";
        public bool EnableAutoCommit { get; set; } = true;
        public int QueuedMinMessages { get; set; } = 1000000;
        public int SessionTimeoutMs { get; set; } = 6000;

    }
}
