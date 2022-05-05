namespace FPTS.FIT.BDRD.Services.Ordering.App
{
    public class OrderingSettings
    {
        public DatabaseType DatabaseType { get; set; } = DatabaseType.InMemory;
    }

    public enum DatabaseType
    {
        InMemory,
        Mssql,
        Oracle
    }
}
