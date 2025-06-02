using System;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using SampleEcomStoreApi.Services;
using SampleEcomStoreApi.DataAccess.Repositories;
using SampleEcomStoreApi.BusinessLogic.Managers;
using SampleEcomStoreApi.Common.Logging;

namespace SampleEcomStoreApi.TestHost
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
                
                Console.WriteLine("Product Service is running at http://localhost:8731/ProductService/");
                Console.WriteLine("WSDL available at http://localhost:8731/ProductService/?wsdl");
                
                // Test the service by making a simple HTTP request
                Task.Run(async () =>
                {
                    await Task.Delay(2000); // Wait 2 seconds for service to fully start
                    try
                    {
                        using (var client = new System.Net.WebClient())
                        {
                            string wsdl = client.DownloadString("http://localhost:8731/ProductService/?wsdl");
                            Console.WriteLine($"WSDL Response Length: {wsdl.Length} characters");
                            Console.WriteLine("✓ WSDL endpoint is responding correctly!");
                            
                            if (wsdl.Contains("ProductService") && wsdl.Contains("wsdl:definitions"))
                            {
                                Console.WriteLine("✓ WSDL contains expected content!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"✗ Error testing WSDL endpoint: {ex.Message}");
                    }
                });
                
                Console.WriteLine("Service will run for 60 seconds...");
                Thread.Sleep(60000); // Run for 60 seconds
                
                StopService();
                Console.WriteLine("Service stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            finally
            {
                // Remove the ReadKey to avoid console input issues
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
            var baseAddress = new Uri("http://localhost:8731/ProductService/");
            productServiceHost = new ServiceHost(container.Resolve<ProductService>(), baseAddress);
            
            var binding = new BasicHttpBinding();
            productServiceHost.AddServiceEndpoint(
                typeof(SampleEcomStoreApi.Contracts.ServiceContracts.IProductService),
                binding,
                "");
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
