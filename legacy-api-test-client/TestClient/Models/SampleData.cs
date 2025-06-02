using TestClient.ServiceProxies;

namespace TestClient.Models
{
    public static class SampleData
    {
        public static CustomerDto GetSampleCustomer()
        {
            return new CustomerDto
            {
                CustomerId = 0, // Will be set by service
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@example.com",
                Phone = "555-123-4567",
                Address = "123 Main Street",
                City = "New York",
                State = "NY",
                ZipCode = "10001",
                Country = "USA",
                IsActive = true,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static ProductDto GetSampleProduct()
        {
            return new ProductDto
            {
                ProductId = 0, // Will be set by service
                Name = "Sample Laptop",
                Description = "High-performance laptop for testing",
                Price = 1299.99m,
                Category = "Electronics",
                StockQuantity = 10,
                IsActive = true,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static List<CustomerDto> GetSampleCustomers()
        {
            return new List<CustomerDto>
            {
                new CustomerDto
                {
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Email = "alice.johnson@example.com",
                    Phone = "555-234-5678",
                    Address = "456 Oak Avenue",
                    City = "Los Angeles",
                    State = "CA",
                    ZipCode = "90210",
                    Country = "USA",
                    IsActive = true
                },
                new CustomerDto
                {
                    FirstName = "Bob",
                    LastName = "Wilson",
                    Email = "bob.wilson@example.com",
                    Phone = "555-345-6789",
                    Address = "789 Pine Street",
                    City = "Chicago",
                    State = "IL",
                    ZipCode = "60601",
                    Country = "USA",
                    IsActive = true
                },
                new CustomerDto
                {
                    FirstName = "Carol",
                    LastName = "Davis",
                    Email = "carol.davis@example.com",
                    Phone = "555-456-7890",
                    Address = "321 Elm Drive",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77001",
                    Country = "USA",
                    IsActive = true
                }
            };
        }

        public static List<ProductDto> GetSampleProducts()
        {
            return new List<ProductDto>
            {
                new ProductDto
                {
                    Name = "Gaming Mouse",
                    Description = "High-precision gaming mouse",
                    Price = 79.99m,
                    Category = "Electronics",
                    StockQuantity = 25,
                    IsActive = true
                },
                new ProductDto
                {
                    Name = "Programming Book",
                    Description = "Learn C# programming",
                    Price = 49.99m,
                    Category = "Books",
                    StockQuantity = 15,
                    IsActive = true
                },
                new ProductDto
                {
                    Name = "Wireless Headphones",
                    Description = "Noise-cancelling wireless headphones",
                    Price = 199.99m,
                    Category = "Electronics",
                    StockQuantity = 8,
                    IsActive = true
                }
            };
        }
    }
}
