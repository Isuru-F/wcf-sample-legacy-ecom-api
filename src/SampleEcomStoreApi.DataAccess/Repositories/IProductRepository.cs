using System.Collections.Generic;
using SampleEcomStoreApi.DataAccess.Entities;

namespace SampleEcomStoreApi.DataAccess.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(int productId);
        List<Product> GetByCategory(string category);
        List<Product> Search(string searchTerm);
        int Create(Product product);
        bool Update(Product product);
        bool Delete(int productId);
        bool UpdateStock(int productId, int quantity);
    }
}
