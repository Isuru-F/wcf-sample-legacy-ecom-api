using System.Collections.Generic;
using System.ServiceModel;
using SampleEcomStoreApi.Contracts.ServiceContracts;
using SampleEcomStoreApi.Contracts.DataContracts;

namespace SampleEcomStoreApi.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CustomerService : ICustomerService
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
