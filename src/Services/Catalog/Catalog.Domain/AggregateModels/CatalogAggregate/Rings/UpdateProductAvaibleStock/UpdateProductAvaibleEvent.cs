using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel;

namespace FPTS.FIT.BDRD.Services.Catalog.Domain.AggregateModels.CatalogAggregate.Rings.UpdateProductAvaibleStock
#nullable disable
{
    public class UpdateProductAvaibleStockEvent : BaseRingEvent
    {
        public IEnumerable<int> ProductIds { get; set; }
        public string ReplyAddress { get; set; }
    }
}
