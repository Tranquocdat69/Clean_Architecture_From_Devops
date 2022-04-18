namespace ECom.BuildingBlocks.LogLib.KafkaLogger.Configs
{
    public class Target
    {
        public string LogTemplate { get; set; }                  = "{date} {level:uppercase=true:truncate=short} {logger} {message}";
        public string Topic { get; set; }                        = "kafka-logger";
        public int Partition { get; set; }                       = -1;
        public LogTemplateFormat LogTemplateFormat { get; set; } = new LogTemplateFormat();
    }
}
