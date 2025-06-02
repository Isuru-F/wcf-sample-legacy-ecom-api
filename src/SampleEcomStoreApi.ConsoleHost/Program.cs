using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using SampleEcomStoreApi.Services;
using SampleEcomStoreApi.DataAccess.Repositories;
using SampleEcomStoreApi.BusinessLogic.Managers;
using SampleEcomStoreApi.Common.Logging;

namespace SampleEcomStoreApi.ConsoleHost
{
    class Program
    {
        private static IWindsorContainer container;
        private static ServiceHost productServiceHost;
        private static ServiceHost customerServiceHost;
        private static ServiceHost orderServiceHost;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting Sample Ecommerce Store API Console Host...");
                
                InitializeContainer();
                StartServices();
                
                Console.WriteLine("Services are running. Press Enter to stop...");
                Console.WriteLine();
                Console.WriteLine("Available endpoints:");
                Console.WriteLine("Product Service: http://localhost:8731/ProductService/");
                Console.WriteLine("Customer Service: http://localhost:8732/CustomerService/");
                Console.WriteLine("Order Service: http://localhost:8733/OrderService/");
                Console.WriteLine();
                Console.WriteLine("WSDL URLs:");
                Console.WriteLine("Product Service WSDL: http://localhost:8731/ProductService/?wsdl");
                Console.WriteLine("Customer Service WSDL: http://localhost:8732/CustomerService/?wsdl");
                Console.WriteLine("Order Service WSDL: http://localhost:8733/OrderService/?wsdl");
                
                Console.ReadLine();
                
                StopServices();
                Console.WriteLine("Services stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Press Enter to exit...");
                Console.ReadLine();
            }
        }

        private static void InitializeContainer()
        {
            container = new WindsorContainer();
            
            // Register repositories
            container.Register(Component.For<IProductRepository>().ImplementedBy<SimpleProductRepository>());
            container.Register(Component.For<ICustomerRepository>().ImplementedBy<SimpleCustomerRepository>());
            container.Register(Component.For<IOrderRepository>().ImplementedBy<SimpleOrderRepository>());
            
            // Register business managers
            container.Register(Component.For<IProductManager>().ImplementedBy<ProductManager>());
            container.Register(Component.For<ICustomerManager>().ImplementedBy<CustomerManager>());
            container.Register(Component.For<IOrderManager>().ImplementedBy<OrderManager>());
            
            // Register logging
            container.Register(Component.For<ILogger>().ImplementedBy<EnterpriseLibraryLogger>());
            
            // Register services
            container.Register(Component.For<ProductService>().LifestyleSingleton());
            container.Register(Component.For<CustomerService>().LifestyleSingleton());
            container.Register(Component.For<OrderService>().LifestyleSingleton());
        }

        private static void StartServices()
        {
            // Product Service
            productServiceHost = new ServiceHost(container.Resolve<ProductService>(), new Uri("http://localhost:8731/ProductService/"));
            productServiceHost.AddServiceEndpoint(
                typeof(SampleEcomStoreApi.Contracts.ServiceContracts.IProductService),
                new BasicHttpBinding(),
                "");
            EnableMetadata(productServiceHost);
            productServiceHost.Open();
            Console.WriteLine("Product Service started at http://localhost:8731/ProductService/");

            // Customer Service
            customerServiceHost = new ServiceHost(container.Resolve<CustomerService>(), new Uri("http://localhost:8732/CustomerService/"));
            customerServiceHost.AddServiceEndpoint(
                typeof(SampleEcomStoreApi.Contracts.ServiceContracts.ICustomerService),
                new BasicHttpBinding(),
                "");
            EnableMetadata(customerServiceHost);
            customerServiceHost.Open();
            Console.WriteLine("Customer Service started at http://localhost:8732/CustomerService/");

            // Order Service
            orderServiceHost = new ServiceHost(container.Resolve<OrderService>(), new Uri("http://localhost:8733/OrderService/"));
            orderServiceHost.AddServiceEndpoint(
                typeof(SampleEcomStoreApi.Contracts.ServiceContracts.IOrderService),
                new BasicHttpBinding(),
                "");
            EnableMetadata(orderServiceHost);
            orderServiceHost.Open();
            Console.WriteLine("Order Service started at http://localhost:8733/OrderService/");
        }

        private static void EnableMetadata(ServiceHost serviceHost)
        {
            ServiceMetadataBehavior metadataBehavior = serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (metadataBehavior == null)
            {
                metadataBehavior = new ServiceMetadataBehavior();
                serviceHost.Description.Behaviors.Add(metadataBehavior);
            }
            metadataBehavior.HttpGetEnabled = true;
        }

        private static void StopServices()
        {
            if (productServiceHost != null)
            {
                productServiceHost.Close();
                productServiceHost = null;
            }

            if (customerServiceHost != null)
            {
                customerServiceHost.Close();
                customerServiceHost = null;
            }

            if (orderServiceHost != null)
            {
                orderServiceHost.Close();
                orderServiceHost = null;
            }

            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }
    }
}
