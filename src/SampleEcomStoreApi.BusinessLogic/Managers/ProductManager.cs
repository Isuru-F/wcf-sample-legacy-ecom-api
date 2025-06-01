using System.Collections.Generic;
using SampleEcomStoreApi.Contracts.DataContracts;
using SampleEcomStoreApi.DataAccess.Repositories;
using SampleEcomStoreApi.DataAccess.Entities;
using SampleEcomStoreApi.Common.Logging;
using SampleEcomStoreApi.Common.Validation;

namespace SampleEcomStoreApi.BusinessLogic.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public ProductManager(IProductRepository productRepository, ILogger logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public List<ProductDto> GetAllProducts()
        {
            _logger.LogInfo("Getting all products");
            var products = _productRepository.GetAll();
            return MapToProductDtos(products);
        }

        public ProductDto GetProductById(int productId)
        {
            _logger.LogInfo($"Getting product by ID: {productId}");
            var product = _productRepository.GetById(productId);
            return product != null ? MapToProductDto(product) : null;
        }

        public List<ProductDto> GetProductsByCategory(string category)
        {
            _logger.LogInfo($"Getting products by category: {category}");
            var products = _productRepository.GetByCategory(category);
            return MapToProductDtos(products);
        }

        public List<ProductDto> SearchProducts(string searchTerm)
        {
            _logger.LogInfo($"Searching products with term: {searchTerm}");
            var products = _productRepository.Search(searchTerm);
            return MapToProductDtos(products);
        }

        public int CreateProduct(ProductDto productDto)
        {
            _logger.LogInfo($"Creating product: {productDto.Name}");
            ValidationHelper.ValidateObject(productDto);
            
            var product = MapToProduct(productDto);
            return _productRepository.Create(product);
        }

        public bool UpdateProduct(ProductDto productDto)
        {
            _logger.LogInfo($"Updating product: {productDto.ProductId}");
            ValidationHelper.ValidateObject(productDto);
            
            var product = MapToProduct(productDto);
            return _productRepository.Update(product);
        }

        public bool DeleteProduct(int productId)
        {
            _logger.LogInfo($"Deleting product: {productId}");
            return _productRepository.Delete(productId);
        }

        public bool UpdateStock(int productId, int quantity)
        {
            _logger.LogInfo($"Updating stock for product {productId} to {quantity}");
            return _productRepository.UpdateStock(productId, quantity);
        }

        private ProductDto MapToProductDto(Product product)
        {
            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                CreatedDate = product.CreatedDate,
                ModifiedDate = product.ModifiedDate,
                IsActive = product.IsActive
            };
        }

        private Product MapToProduct(ProductDto productDto)
        {
            return new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Category = productDto.Category,
                StockQuantity = productDto.StockQuantity,
                ImageUrl = productDto.ImageUrl,
                CreatedDate = productDto.CreatedDate,
                ModifiedDate = productDto.ModifiedDate,
                IsActive = productDto.IsActive
            };
        }

        private List<ProductDto> MapToProductDtos(List<Product> products)
        {
            var productDtos = new List<ProductDto>();
            foreach (var product in products)
            {
                productDtos.Add(MapToProductDto(product));
            }
            return productDtos;
        }
    }
}
