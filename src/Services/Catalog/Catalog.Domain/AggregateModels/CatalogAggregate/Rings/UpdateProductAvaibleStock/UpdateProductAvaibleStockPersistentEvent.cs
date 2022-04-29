using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel;

namespace ECom.Services.Catalog.Domain.AggregateModels.CatalogAggregate.Rings.UpdateProductAvaibleStock
#nullable disable
{
    public class UpdateProductAvaibleStockPersistentEvent : BaseRingEvent
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
