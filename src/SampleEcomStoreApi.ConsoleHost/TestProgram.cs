using System;
using System.Threading;
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
    class TestProgram
    {
        private static IWindsorContainer container;
        private static ServiceHost productServiceHost;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting test of Product Service...");
                
                InitializeContainer();
                StartProductService();
                
                Console.WriteLine("Product Service is running at http://localhost:8001/ProductService");
                Console.WriteLine("WSDL available at http://localhost:8001/ProductService?wsdl");
                Console.WriteLine("Service will run for 30 seconds...");
                
                Thread.Sleep(30000); // Run for 30 seconds
                
                StopService();
                Console.WriteLine("Service stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
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
        }

        private static void StartProductService()
        {
            productServiceHost = new ServiceHost(container.Resolve<ProductService>(), new Uri("http://localhost:8001/"));
            productServiceHost.AddServiceEndpoint(
                typeof(SampleEcomStoreApi.Contracts.ServiceContracts.IProductService),
                new BasicHttpBinding(),
                "ProductService");
            EnableMetadata(productServiceHost);
            productServiceHost.Open();
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

        private static void StopService()
        {
            if (productServiceHost != null)
            {
                productServiceHost.Close();
                productServiceHost = null;
            }

            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }
    }
}
