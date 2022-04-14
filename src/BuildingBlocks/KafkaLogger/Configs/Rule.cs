namespace ECom.BuildingBlocks.LogLib.KafkaLogger.Configs
{
    public class Rule
    {
        public string Logger { get; set; }   = "*";
        public string MinLevel { get; set; } = "trace";
        public string MaxLevel { get; set; } = "crit";
        public string WriteTo { get; set; }  = "Target1";
    }
}
