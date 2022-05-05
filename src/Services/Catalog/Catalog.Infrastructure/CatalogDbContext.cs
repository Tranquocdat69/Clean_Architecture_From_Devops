using FPTS.FIT.BDRD.Services.Catalog.Domain.AggregateModels.CatalogAggregate;
using FPTS.FIT.BDRD.Services.Catalog.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace FPTS.FIT.BDRD.Services.Catalog.Infrastructure
#nullable disable
{
    public class CatalogDbContext : DbContext
    {
        public static string DEFAULT_SCHEMA = "catalog";
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {

        }

        DbSet<CatalogType> CatalogTypes { get; set; }
        DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogEntityTypeConfiguration());
        }
    }
}
