namespace ECom.BuildingBlocks.LogLib.KafkaLogger.Configs
{
    public class LogTemplateFormat
    {
        public LogLevelNameFormat LevelNameFormat { get; set; } = new LogLevelNameFormat();
        public string DatetimeFormat { get; set; }              = "yyyy-MM-dd hh:mm:ss.ffff";
    }
}
