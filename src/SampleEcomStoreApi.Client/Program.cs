using System;
using System.ServiceModel;
using SampleEcomStoreApi.Contracts.ServiceContracts;
using SampleEcomStoreApi.Contracts.DataContracts;

namespace SampleEcomStoreApi.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sample Ecommerce Store API Client");
            Console.WriteLine("=================================");
            
            try
            {
                TestProductService();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void TestProductService()
        {
            Console.WriteLine("Testing Product Service...");
            
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:8080/ProductService.svc");
            
            using (var channelFactory = new ChannelFactory<IProductService>(binding, endpoint))
            {
                var productService = channelFactory.CreateChannel();
                
                try
                {
                    // Test GetAllProducts
                    Console.WriteLine("Getting all products...");
                    var products = productService.GetAllProducts();
                    Console.WriteLine($"Found {products.Count} products");
                    
                    // Test CreateProduct
                    Console.WriteLine("Creating a test product...");
                    var newProduct = new ProductDto
                    {
                        Name = "Test Product from Client",
                        Description = "This is a test product created by the client",
                        Price = 99.99m,
                        Category = "Electronics",
                        StockQuantity = 25,
                        ImageUrl = "http://example.com/image.jpg",
                        IsActive = true
                    };
                    
                    var productId = productService.CreateProduct(newProduct);
                    Console.WriteLine($"Created product with ID: {productId}");
                    
                    // Test GetProductById
                    if (productId > 0)
                    {
                        Console.WriteLine($"Getting product by ID: {productId}");
                        var retrievedProduct = productService.GetProductById(productId);
                        if (retrievedProduct != null)
                        {
                            Console.WriteLine($"Retrieved: {retrievedProduct.Name} - ${retrievedProduct.Price}");
                        }
                    }
                    
                    Console.WriteLine("Product service tests completed successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Service error: {ex.Message}");
                }
            }
        }
    }
}
