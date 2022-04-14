using ECom.BuildingBlocks.SharedKernel;
using ECom.Services.Catalog.Domain.Exceptions;

namespace ECom.Services.Catalog.Domain.AggregateModels.CatalogAggregate
#nullable disable
{
    public class Product : BaseEntity
    {
        public Product(int id, string name, int quantity)
        {
            Id = id;
            Name = name;
            AvailableStock = quantity;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int AvailableStock { get; private set; }

        public void UpdateStock(int quantity)
        {
            AvailableStock += quantity;
        }
    }
}
