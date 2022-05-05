using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel;

namespace FPTS.FIT.BDRD.Services.Catalog.Domain.AggregateModels.CatalogAggregate.Rings.UpdateProductAvaibleStock
#nullable disable
{
    public class UpdateProductAvaibleStockReplyEvent : BaseRingEvent
    {
        public string Message { get; set; }
        public string ReplyAddress { get; set; }
        public bool IsSuccess { get; set; }
    }
}
