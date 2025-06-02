using System.Collections.Generic;
using System.ServiceModel;
using SampleEcomStoreApi.Contracts.ServiceContracts;
using SampleEcomStoreApi.Contracts.DataContracts;
using SampleEcomStoreApi.BusinessLogic.Managers;
using SampleEcomStoreApi.Common.Logging;

namespace SampleEcomStoreApi.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ProductService : IProductService
    {
        private readonly IProductManager _productManager;
        private readonly ILogger _logger;

        public ProductService(IProductManager productManager, ILogger logger)
        {
            _productManager = productManager;
            _logger = logger;
        }

        public List<ProductDto> GetAllProducts()
        {
            try
            {
                _logger.LogInfo("ProductService.GetAllProducts called");
                return _productManager.GetAllProducts();
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error in ProductService.GetAllProducts", ex);
                throw new FaultException("An error occurred while retrieving products");
            }
        }

        public ProductDto GetProductById(int productId)
        {
            try
            {
                _logger.LogInfo($"ProductService.GetProductById called with ID: {productId}");
                return _productManager.GetProductById(productId);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in ProductService.GetProductById for ID: {productId}", ex);
                throw new FaultException("An error occurred while retrieving the product");
            }
        }

        public List<ProductDto> GetProductsByCategory(string category)
        {
            try
            {
                _logger.LogInfo($"ProductService.GetProductsByCategory called with category: {category}");
                return _productManager.GetProductsByCategory(category);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in ProductService.GetProductsByCategory for category: {category}", ex);
                throw new FaultException("An error occurred while retrieving products by category");
            }
        }

        public List<ProductDto> SearchProducts(string searchTerm)
        {
            try
            {
                _logger.LogInfo($"ProductService.SearchProducts called with term: {searchTerm}");
                return _productManager.SearchProducts(searchTerm);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in ProductService.SearchProducts for term: {searchTerm}", ex);
                throw new FaultException("An error occurred while searching products");
            }
        }

        public int CreateProduct(ProductDto product)
        {
            try
            {
                _logger.LogInfo($"ProductService.CreateProduct called for: {product?.Name}");
                return _productManager.CreateProduct(product);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in ProductService.CreateProduct for: {product?.Name}", ex);
                throw new FaultException("An error occurred while creating the product");
            }
        }

        public bool UpdateProduct(ProductDto product)
        {
            try
            {
                _logger.LogInfo($"ProductService.UpdateProduct called for ID: {product?.ProductId}");
                return _productManager.UpdateProduct(product);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in ProductService.UpdateProduct for ID: {product?.ProductId}", ex);
                throw new FaultException("An error occurred while updating the product");
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                _logger.LogInfo($"ProductService.DeleteProduct called for ID: {productId}");
                return _productManager.DeleteProduct(productId);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in ProductService.DeleteProduct for ID: {productId}", ex);
                throw new FaultException("An error occurred while deleting the product");
            }
        }

        public bool UpdateStock(int productId, int quantity)
        {
            try
            {
                _logger.LogInfo($"ProductService.UpdateStock called for ID: {productId}, quantity: {quantity}");
                return _productManager.UpdateStock(productId, quantity);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in ProductService.UpdateStock for ID: {productId}", ex);
                throw new FaultException("An error occurred while updating product stock");
            }
        }
    }
}
