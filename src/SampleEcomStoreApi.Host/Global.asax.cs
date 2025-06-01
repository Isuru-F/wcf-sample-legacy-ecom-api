using System;
using System.Web;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using SampleEcomStoreApi.DataAccess.Repositories;
using SampleEcomStoreApi.BusinessLogic.Managers;
using SampleEcomStoreApi.Common.Logging;

namespace SampleEcomStoreApi.Host
{
    public class Global : HttpApplication
    {
        private static IWindsorContainer container;

        void Application_Start(object sender, EventArgs e)
        {
            InitializeContainer();
        }

        private void InitializeContainer()
        {
            container = new WindsorContainer();
            
            // Register repositories
            container.Register(Component.For<IProductRepository>().ImplementedBy<SimpleProductRepository>());
            container.Register(Component.For<ICustomerRepository>().ImplementedBy<CustomerRepository>());
            container.Register(Component.For<IOrderRepository>().ImplementedBy<OrderRepository>());
            
            // Register business managers
            container.Register(Component.For<IProductManager>().ImplementedBy<ProductManager>());
            container.Register(Component.For<ICustomerManager>().ImplementedBy<CustomerManager>());
            container.Register(Component.For<IOrderManager>().ImplementedBy<OrderManager>());
            
            // Register logging
            container.Register(Component.For<ILogger>().ImplementedBy<EnterpriseLibraryLogger>());
        }

        void Application_End(object sender, EventArgs e)
        {
            if (container != null)
            {
                container.Dispose();
            }
        }

        public static IWindsorContainer Container
        {
            get { return container; }
        }
    }
}
