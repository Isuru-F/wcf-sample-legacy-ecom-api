using System.Collections.Generic;
using SampleEcomStoreApi.Contracts.DataContracts;

namespace SampleEcomStoreApi.BusinessLogic.Managers
{
    public class CustomerManager : ICustomerManager
    {
        public List<CustomerDto> GetAllCustomers()
        {
            return new List<CustomerDto>();
        }

        public CustomerDto GetCustomerById(int customerId)
        {
            return new CustomerDto();
        }

        public CustomerDto GetCustomerByEmail(string email)
        {
            return new CustomerDto();
        }

        public int CreateCustomer(CustomerDto customer)
        {
            return 1;
        }

        public bool UpdateCustomer(CustomerDto customer)
        {
            return true;
        }

        public bool DeleteCustomer(int customerId)
        {
            return true;
        }

        public bool DeactivateCustomer(int customerId)
        {
            return true;
        }
    }
}
