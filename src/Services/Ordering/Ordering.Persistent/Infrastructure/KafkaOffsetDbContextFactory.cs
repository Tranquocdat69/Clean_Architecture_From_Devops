namespace ECom.Services.Ordering.Persistent.Infrastructure
{
    public class KafkaOffsetDbContextFactory : IDesignTimeDbContextFactory<KafkaOffsetDbContext>
    {
        public KafkaOffsetDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<KafkaOffsetDbContext>();
            var connectionString = configuration.GetConnectionString(ServiceCollectionExtensions.DB_CONNECTION_KEY);
            optionsBuilder.UseSqlServer(connectionString);

            return new KafkaOffsetDbContext(optionsBuilder.Options);
        }
    }
}
