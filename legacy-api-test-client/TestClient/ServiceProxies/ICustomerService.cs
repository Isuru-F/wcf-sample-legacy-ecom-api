using System.ServiceModel;

namespace TestClient.ServiceProxies
{
    [ServiceContract(Namespace = "http://tempuri.org/")]
    public interface ICustomerService
    {
        [OperationContract(Action = "http://tempuri.org/ICustomerService/GetAllCustomers")]
        Task<List<CustomerDto>> GetAllCustomersAsync();

        [OperationContract(Action = "http://tempuri.org/ICustomerService/GetCustomerById")]
        Task<CustomerDto> GetCustomerByIdAsync(int customerId);

        [OperationContract(Action = "http://tempuri.org/ICustomerService/GetCustomerByEmail")]
        Task<CustomerDto> GetCustomerByEmailAsync(string email);

        [OperationContract(Action = "http://tempuri.org/ICustomerService/CreateCustomer")]
        Task<int> CreateCustomerAsync(CustomerDto customer);

        [OperationContract(Action = "http://tempuri.org/ICustomerService/UpdateCustomer")]
        Task<bool> UpdateCustomerAsync(CustomerDto customer);

        [OperationContract(Action = "http://tempuri.org/ICustomerService/DeleteCustomer")]
        Task<bool> DeleteCustomerAsync(int customerId);

        [OperationContract(Action = "http://tempuri.org/ICustomerService/DeactivateCustomer")]
        Task<bool> DeactivateCustomerAsync(int customerId);
    }
}
