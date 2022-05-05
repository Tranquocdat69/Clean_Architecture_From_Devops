using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel.Interfaces;

namespace FPTS.FIT.BDRD.Services.Catalog.Domain.AggregateModels.CatalogAggregate
{
    public interface ICatalogRepository : IKeyValuePairRepository<CatalogType, int>
    {
    }
}
