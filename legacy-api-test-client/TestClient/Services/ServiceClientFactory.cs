using System.ServiceModel;
using TestClient.ServiceProxies;

namespace TestClient.Services
{
    public class ServiceClientFactory
    {
        private readonly string _baseUrl;

        public ServiceClientFactory(string baseUrl = "http://localhost")
        {
            _baseUrl = baseUrl;
        }

        public ChannelFactory<ICustomerService> CreateCustomerServiceFactory()
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress($"{_baseUrl}:8732/CustomerService/");
            return new ChannelFactory<ICustomerService>(binding, endpoint);
        }

        public ChannelFactory<IProductService> CreateProductServiceFactory()
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress($"{_baseUrl}:8731/ProductService/");
            return new ChannelFactory<IProductService>(binding, endpoint);
        }

        public ICustomerService CreateCustomerService()
        {
            var factory = CreateCustomerServiceFactory();
            return factory.CreateChannel();
        }

        public IProductService CreateProductService()
        {
            var factory = CreateProductServiceFactory();
            return factory.CreateChannel();
        }
    }
}
