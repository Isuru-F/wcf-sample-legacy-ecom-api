using System.Collections.Generic;
using System.ServiceModel;
using SampleEcomStoreApi.Contracts.DataContracts;

namespace SampleEcomStoreApi.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        List<CustomerDto> GetAllCustomers();

        [OperationContract]
        CustomerDto GetCustomerById(int customerId);

        [OperationContract]
        CustomerDto GetCustomerByEmail(string email);

        [OperationContract]
        int CreateCustomer(CustomerDto customer);

        [OperationContract]
        bool UpdateCustomer(CustomerDto customer);

        [OperationContract]
        bool DeleteCustomer(int customerId);

        [OperationContract]
        bool DeactivateCustomer(int customerId);
    }
}
