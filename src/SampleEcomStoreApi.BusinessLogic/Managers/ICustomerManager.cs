using System.Collections.Generic;
using SampleEcomStoreApi.Contracts.DataContracts;

namespace SampleEcomStoreApi.BusinessLogic.Managers
{
    public interface ICustomerManager
    {
        List<CustomerDto> GetAllCustomers();
        CustomerDto GetCustomerById(int customerId);
        CustomerDto GetCustomerByEmail(string email);
        int CreateCustomer(CustomerDto customer);
        bool UpdateCustomer(CustomerDto customer);
        bool DeleteCustomer(int customerId);
        bool DeactivateCustomer(int customerId);
    }
}
