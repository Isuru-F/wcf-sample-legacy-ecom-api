using System.ServiceModel;

namespace TestClient.ServiceProxies
{
    [ServiceContract(Namespace = "http://tempuri.org/")]
    public interface IProductService
    {
        [OperationContract(Action = "http://tempuri.org/IProductService/GetAllProducts")]
        Task<List<ProductDto>> GetAllProductsAsync();

        [OperationContract(Action = "http://tempuri.org/IProductService/GetProductById")]
        Task<ProductDto> GetProductByIdAsync(int productId);

        [OperationContract(Action = "http://tempuri.org/IProductService/CreateProduct")]
        Task<int> CreateProductAsync(ProductDto product);

        [OperationContract(Action = "http://tempuri.org/IProductService/UpdateProduct")]
        Task<bool> UpdateProductAsync(ProductDto product);

        [OperationContract(Action = "http://tempuri.org/IProductService/DeleteProduct")]
        Task<bool> DeleteProductAsync(int productId);

        [OperationContract(Action = "http://tempuri.org/IProductService/DeactivateProduct")]
        Task<bool> DeactivateProductAsync(int productId);

        [OperationContract(Action = "http://tempuri.org/IProductService/GetProductsByCategory")]
        Task<List<ProductDto>> GetProductsByCategoryAsync(string category);
    }
}
