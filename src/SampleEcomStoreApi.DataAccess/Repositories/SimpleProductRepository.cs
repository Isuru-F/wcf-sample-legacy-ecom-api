using System;
using System.Collections.Generic;
using System.Linq;
using SampleEcomStoreApi.DataAccess.Context;
using SampleEcomStoreApi.DataAccess.Entities;

namespace SampleEcomStoreApi.DataAccess.Repositories
{
    public class SimpleProductRepository : IProductRepository
    {
        public List<Product> GetAll()
        {
            using (var context = new EcommerceDbContext())
            {
                return context.Products.Where(p => p.IsActive).ToList();
            }
        }

        public Product GetById(int productId)
        {
            using (var context = new EcommerceDbContext())
            {
                return context.Products.FirstOrDefault(p => p.ProductId == productId && p.IsActive);
            }
        }

        public List<Product> GetByCategory(string category)
        {
            using (var context = new EcommerceDbContext())
            {
                return context.Products
                    .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase) && p.IsActive)
                    .ToList();
            }
        }

        public List<Product> Search(string searchTerm)
        {
            using (var context = new EcommerceDbContext())
            {
                return context.Products
                    .Where(p => (p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm)) && p.IsActive)
                    .ToList();
            }
        }

        public int Create(Product product)
        {
            using (var context = new EcommerceDbContext())
            {
                product.ProductId = context.Products.Count() + 1;
                product.CreatedDate = DateTime.Now;
                product.ModifiedDate = DateTime.Now;
                product.IsActive = true;

                context.Products.Add(product);
                context.SaveChanges();
                return product.ProductId;
            }
        }

        public bool Update(Product product)
        {
            using (var context = new EcommerceDbContext())
            {
                var existingProduct = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (existingProduct == null) return false;

                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Category = product.Category;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.ModifiedDate = DateTime.Now;

                context.SaveChanges();
                return true;
            }
        }

        public bool Delete(int productId)
        {
            using (var context = new EcommerceDbContext())
            {
                var product = context.Products.FirstOrDefault(p => p.ProductId == productId);
                if (product == null) return false;

                product.IsActive = false;
                product.ModifiedDate = DateTime.Now;

                context.SaveChanges();
                return true;
            }
        }

        public bool UpdateStock(int productId, int quantity)
        {
            using (var context = new EcommerceDbContext())
            {
                var product = context.Products.FirstOrDefault(p => p.ProductId == productId);
                if (product == null) return false;

                product.StockQuantity = quantity;
                product.ModifiedDate = DateTime.Now;

                context.SaveChanges();
                return true;
            }
        }
    }
}
