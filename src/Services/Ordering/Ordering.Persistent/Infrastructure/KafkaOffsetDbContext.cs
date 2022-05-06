namespace ECom.Services.Ordering.Persistent.Infrastructure
#nullable disable
{
    public class KafkaOffsetDbContext : DbContext
    {
        public KafkaOffsetDbContext(DbContextOptions<KafkaOffsetDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<KafkaOffset> KafkaOffsets { get; set; }
    }
}
