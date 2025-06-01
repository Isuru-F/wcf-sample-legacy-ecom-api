using System.Collections.Generic;
using SampleEcomStoreApi.DataAccess.Entities;

namespace SampleEcomStoreApi.DataAccess.Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer GetById(int customerId);
        Customer GetByEmail(string email);
        int Create(Customer customer);
        bool Update(Customer customer);
        bool Delete(int customerId);
        bool Deactivate(int customerId);
    }
}
