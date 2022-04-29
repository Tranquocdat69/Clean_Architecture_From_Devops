using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel.Interfaces;

namespace ECom.Services.Catalog.Domain.AggregateModels.CatalogAggregate
{
    public interface ICatalogRepository : IKeyValuePairRepository<CatalogType, int>
    {
    }
}
