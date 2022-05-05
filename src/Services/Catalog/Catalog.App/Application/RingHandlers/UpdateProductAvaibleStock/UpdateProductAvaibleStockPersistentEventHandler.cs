using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel.Interfaces;
using FPTS.FIT.BDRD.Services.Catalog.Domain.AggregateModels.CatalogAggregate.Rings.UpdateProductAvaibleStock;

namespace FPTS.FIT.BDRD.Services.Catalog.App.Application.RingHandlers.UpdateProductAvaibleStock
{
    public class UpdateProductAvaibleStockPersistentEventHandler : IRingHandler<UpdateProductAvaibleStockPersistentEvent>
    {
        public void OnEvent(UpdateProductAvaibleStockPersistentEvent data, long sequence, bool endOfBatch)
        {
            throw new NotImplementedException();
        }
    }
}
