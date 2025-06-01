using System;
using System.ServiceModel;
using NUnit.Framework;
using SampleEcomStoreApi.Contracts.ServiceContracts;
using SampleEcomStoreApi.Contracts.DataContracts;

namespace SampleEcomStoreApi.IntegrationTests
{
    [TestFixture]
    public class ProductServiceIntegrationTests
    {
        private IProductService _productService;
        private ChannelFactory<IProductService> _channelFactory;

        [SetUp]
        public void Setup()
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:8080/ProductService.svc");
            _channelFactory = new ChannelFactory<IProductService>(binding, endpoint);
            _productService = _channelFactory.CreateChannel();
        }

        [TearDown]
        public void TearDown()
        {
            if (_channelFactory != null)
            {
                try
                {
                    _channelFactory.Close();
                }
                catch
                {
                    _channelFactory.Abort();
                }
                _channelFactory = null;
            }
        }

        [Test]
        [Ignore("Requires running service host")]
        public void GetAllProducts_ServiceRunning_ReturnsProductList()
        {
            // Arrange - service should be running

            // Act
            var result = _productService.GetAllProducts();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        [Ignore("Requires running service host")]
        public void CreateProduct_ValidProduct_ReturnsValidId()
        {
            // Arrange
            var newProduct = new ProductDto
            {
                Name = "Integration Test Product",
                Description = "Created during integration testing",
                Price = 49.99m,
                Category = "Electronics",
                StockQuantity = 15,
                IsActive = true
            };

            // Act
            var productId = _productService.CreateProduct(newProduct);

            // Assert
            Assert.IsTrue(productId > 0);
        }
    }
}
