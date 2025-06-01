using System;
using System.Collections.Generic;
using SampleEcomStoreApi.DataAccess.Entities;

namespace SampleEcomStoreApi.DataAccess.Context
{
    public class EcommerceDbContext : IDisposable
    {
        // In-memory storage for demo purposes
        public static List<Product> Products = new List<Product>();
        public static List<Customer> Customers = new List<Customer>();
        public static List<Order> Orders = new List<Order>();
        public static List<OrderItem> OrderItems = new List<OrderItem>();

        public EcommerceDbContext()
        {
            // Initialize with sample data if empty
            if (Products.Count == 0)
            {
                InitializeSampleData();
            }
        }

        public int SaveChanges()
        {
            // In a real implementation, this would save to database
            return 1;
        }

        public void Dispose()
        {
            // Nothing to dispose in this demo implementation
        }

        private void InitializeSampleData()
        {
            Products.AddRange(new[]
            {
                new Product { ProductId = 1, Name = "Laptop", Description = "High-performance laptop", Price = 999.99m, Category = "Electronics", StockQuantity = 10, IsActive = true, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new Product { ProductId = 2, Name = "Smartphone", Description = "Latest smartphone", Price = 699.99m, Category = "Electronics", StockQuantity = 25, IsActive = true, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new Product { ProductId = 3, Name = "Book", Description = "Programming book", Price = 39.99m, Category = "Books", StockQuantity = 50, IsActive = true, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now }
            });

            Customers.AddRange(new[]
            {
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@email.com", IsActive = true, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@email.com", IsActive = true, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now }
            });
        }
    }
}
