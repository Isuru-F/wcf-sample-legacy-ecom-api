using System;
using System.Collections.Generic;
using System.Data.Entity;
using SampleEcomStoreApi.DataAccess.Entities;

namespace SampleEcomStoreApi.DataAccess.Context
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext() : base("name=EcommerceDb")
        {
            Database.SetInitializer(new EcommerceDbInitializer());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Entity Framework to work with SQLite
            Database.SetInitializer(new EcommerceDbInitializer());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class EcommerceDbInitializer : CreateDatabaseIfNotExists<EcommerceDbContext>
    {
        protected override void Seed(EcommerceDbContext context)
        {
            // Initialize with sample data
            var products = new List<Product>
            {
                new Product { Name = "Laptop", Description = "High-performance laptop", Price = 999.99m, Category = "Electronics", StockQuantity = 10, IsActive = true, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new Product { Name = "Smartphone", Description = "Latest smartphone", Price = 699.99m, Category = "Electronics", StockQuantity = 25, IsActive = true, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new Product { Name = "Book", Description = "Programming book", Price = 39.99m, Category = "Books", StockQuantity = 50, IsActive = true, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now }
            };

            var customers = new List<Customer>
            {
                new Customer { FirstName = "John", LastName = "Doe", Email = "john.doe@email.com", IsActive = true, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new Customer { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@email.com", IsActive = true, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now }
            };

            context.Products.AddRange(products);
            context.Customers.AddRange(customers);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
