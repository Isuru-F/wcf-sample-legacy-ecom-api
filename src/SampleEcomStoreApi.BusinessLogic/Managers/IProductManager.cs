using System.Collections.Generic;
using SampleEcomStoreApi.Contracts.DataContracts;

namespace SampleEcomStoreApi.BusinessLogic.Managers
{
    public interface IProductManager
    {
        List<ProductDto> GetAllProducts();
        ProductDto GetProductById(int productId);
        List<ProductDto> GetProductsByCategory(string category);
        List<ProductDto> SearchProducts(string searchTerm);
        int CreateProduct(ProductDto product);
        bool UpdateProduct(ProductDto product);
        bool DeleteProduct(int productId);
        bool UpdateStock(int productId, int quantity);
    }
}
