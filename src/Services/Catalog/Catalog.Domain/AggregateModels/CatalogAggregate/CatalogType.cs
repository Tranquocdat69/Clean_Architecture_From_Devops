using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel;
using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel.Interfaces;

namespace FPTS.FIT.BDRD.Services.Catalog.Domain.AggregateModels.CatalogAggregate
#nullable disable
{
    public class CatalogType : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        private List<Product> _catalogProducts;
        public IEnumerable<Product> CatalogProducts => _catalogProducts.AsReadOnly();

        public CatalogType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        protected CatalogType()
        {
            _catalogProducts = new List<Product>();
        }

        public Product AddProduct(int productId, string productName, int availableStock, double unitPrice)
        {
            var existProduct = _catalogProducts.FirstOrDefault(x => x.Id == productId);
            if(existProduct != null)
            {
                existProduct.UpdateStock(1);
                return existProduct;
            }

            var product = new Product(productId, productName, availableStock, unitPrice);
            _catalogProducts.Add(product);
            return product;
        }
    }
}
