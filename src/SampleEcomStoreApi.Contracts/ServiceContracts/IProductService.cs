using System.Collections.Generic;
using System.ServiceModel;
using SampleEcomStoreApi.Contracts.DataContracts;

namespace SampleEcomStoreApi.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        List<ProductDto> GetAllProducts();

        [OperationContract]
        ProductDto GetProductById(int productId);

        [OperationContract]
        List<ProductDto> GetProductsByCategory(string category);

        [OperationContract]
        List<ProductDto> SearchProducts(string searchTerm);

        [OperationContract]
        int CreateProduct(ProductDto product);

        [OperationContract]
        bool UpdateProduct(ProductDto product);

        [OperationContract]
        bool DeleteProduct(int productId);

        [OperationContract]
        bool UpdateStock(int productId, int quantity);
    }
}
