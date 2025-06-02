using TestClient.Models;
using TestClient.ServiceProxies;
using TestClient.Services;

namespace TestClient.UI
{
    public class ConsoleUI
    {
        private readonly ServiceClientFactory _serviceFactory;

        public ConsoleUI()
        {
            _serviceFactory = new ServiceClientFactory();
        }

        public async Task RunAsync()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("  Sample Ecommerce Store API Test Client");
            Console.WriteLine("=================================================");
            Console.WriteLine();

            bool running = true;
            while (running)
            {
                DisplayMainMenu();
                var choice = Console.ReadLine();

                try
                {
                    switch (choice?.ToLower())
                    {
                        case "1":
                            await ViewAllCustomersAsync();
                            break;
                        case "2":
                            await CreateCustomerAsync();
                            break;
                        case "3":
                            await ViewAllProductsAsync();
                            break;
                        case "4":
                            await CreateProductAsync();
                            break;
                        case "5":
                            await SearchCustomerAsync();
                            break;
                        case "6":
                            await TestAllServicesAsync();
                            break;
                        case "q":
                        case "quit":
                        case "exit":
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("Thank you for using the test client!");
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. View All Customers");
            Console.WriteLine("2. Create New Customer");
            Console.WriteLine("3. View All Products");
            Console.WriteLine("4. Create New Product");
            Console.WriteLine("5. Search Customer by Email");
            Console.WriteLine("6. Test All Services (Regression Test)");
            Console.WriteLine("Q. Quit");
            Console.WriteLine();
            Console.Write("Choose an option: ");
        }

        private async Task ViewAllCustomersAsync()
        {
            Console.WriteLine("\n=== All Customers ===");
            
            using var factory = _serviceFactory.CreateCustomerServiceFactory();
            var customerService = factory.CreateChannel();

            try
            {
                var customers = await customerService.GetAllCustomersAsync();
                
                if (customers == null || customers.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                    return;
                }

                Console.WriteLine($"Found {customers.Count} customers:\n");
                
                foreach (var customer in customers)
                {
                    DisplayCustomer(customer);
                    Console.WriteLine(new string('-', 50));
                }
            }
            finally
            {
                if (customerService is IDisposable disposable)
                    disposable.Dispose();
            }
        }

        private async Task CreateCustomerAsync()
        {
            Console.WriteLine("\n=== Create New Customer ===");
            
            // Start with sample data
            var customer = SampleData.GetSampleCustomer();
            
            // Allow user to edit the prefilled data
            customer = EditCustomer(customer);
            
            Console.WriteLine("\nCreating customer...");
            
            using var factory = _serviceFactory.CreateCustomerServiceFactory();
            var customerService = factory.CreateChannel();

            try
            {
                var customerId = await customerService.CreateCustomerAsync(customer);
                Console.WriteLine($"SUCCESS! Customer created with ID: {customerId}");
                
                // Display the created customer
                Console.WriteLine("\nCreated customer details:");
                customer.CustomerId = customerId;
                DisplayCustomer(customer);
            }
            finally
            {
                if (customerService is IDisposable disposable)
                    disposable.Dispose();
            }
        }

        private async Task ViewAllProductsAsync()
        {
            Console.WriteLine("\n=== All Products ===");
            
            using var factory = _serviceFactory.CreateProductServiceFactory();
            var productService = factory.CreateChannel();

            try
            {
                var products = await productService.GetAllProductsAsync();
                
                if (products == null || products.Count == 0)
                {
                    Console.WriteLine("No products found.");
                    return;
                }

                Console.WriteLine($"Found {products.Count} products:\n");
                
                foreach (var product in products)
                {
                    DisplayProduct(product);
                    Console.WriteLine(new string('-', 50));
                }
            }
            finally
            {
                if (productService is IDisposable disposable)
                    disposable.Dispose();
            }
        }

        private async Task CreateProductAsync()
        {
            Console.WriteLine("\n=== Create New Product ===");
            
            // Start with sample data
            var product = SampleData.GetSampleProduct();
            
            // Allow user to edit the prefilled data
            product = EditProduct(product);
            
            Console.WriteLine("\nCreating product...");
            
            using var factory = _serviceFactory.CreateProductServiceFactory();
            var productService = factory.CreateChannel();

            try
            {
                var productId = await productService.CreateProductAsync(product);
                Console.WriteLine($"SUCCESS! Product created with ID: {productId}");
                
                // Display the created product
                Console.WriteLine("\nCreated product details:");
                product.ProductId = productId;
                DisplayProduct(product);
            }
            finally
            {
                if (productService is IDisposable disposable)
                    disposable.Dispose();
            }
        }

        private async Task SearchCustomerAsync()
        {
            Console.WriteLine("\n=== Search Customer by Email ===");
            Console.Write("Enter email address: ");
            var email = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty.");
                return;
            }

            using var factory = _serviceFactory.CreateCustomerServiceFactory();
            var customerService = factory.CreateChannel();

            try
            {
                var customer = await customerService.GetCustomerByEmailAsync(email);
                
                if (customer == null)
                {
                    Console.WriteLine($"No customer found with email: {email}");
                    return;
                }

                Console.WriteLine("\nCustomer found:");
                DisplayCustomer(customer);
            }
            finally
            {
                if (customerService is IDisposable disposable)
                    disposable.Dispose();
            }
        }

        private async Task TestAllServicesAsync()
        {
            Console.WriteLine("\n=== Regression Test - All Services ===");
            var results = new List<(string Test, bool Success, string Message)>();

            // Test Customer Service
            Console.WriteLine("Testing Customer Service...");
            try
            {
                using var customerFactory = _serviceFactory.CreateCustomerServiceFactory();
                var customerService = customerFactory.CreateChannel();

                // Test GetAllCustomers
                var customers = await customerService.GetAllCustomersAsync();
                results.Add(("GetAllCustomers", customers != null, customers?.Count.ToString() + " customers found"));

                // Test CreateCustomer
                var testCustomer = SampleData.GetSampleCustomer();
                testCustomer.Email = $"test.{DateTime.Now.Ticks}@regression.com"; // Unique email
                var customerId = await customerService.CreateCustomerAsync(testCustomer);
                results.Add(("CreateCustomer", customerId > 0, $"Customer ID: {customerId}"));

                // Test GetCustomerById
                var retrievedCustomer = await customerService.GetCustomerByIdAsync(customerId);
                results.Add(("GetCustomerById", retrievedCustomer != null, "Customer retrieved"));

                if (customerService is IDisposable disposable)
                    disposable.Dispose();
            }
            catch (Exception ex)
            {
                results.Add(("Customer Service", false, ex.Message));
            }

            // Test Product Service
            Console.WriteLine("Testing Product Service...");
            try
            {
                using var productFactory = _serviceFactory.CreateProductServiceFactory();
                var productService = productFactory.CreateChannel();

                // Test GetAllProducts
                var products = await productService.GetAllProductsAsync();
                results.Add(("GetAllProducts", products != null, products?.Count.ToString() + " products found"));

                // Test CreateProduct
                var testProduct = SampleData.GetSampleProduct();
                testProduct.Name = $"Test Product {DateTime.Now.Ticks}"; // Unique name
                var productId = await productService.CreateProductAsync(testProduct);
                results.Add(("CreateProduct", productId > 0, $"Product ID: {productId}"));

                if (productService is IDisposable disposable)
                    disposable.Dispose();
            }
            catch (Exception ex)
            {
                results.Add(("Product Service", false, ex.Message));
            }

            // Display Results
            Console.WriteLine("\n=== Test Results ===");
            var passed = 0;
            var total = results.Count;

            foreach (var (test, success, message) in results)
            {
                var status = success ? "PASS" : "FAIL";
                var color = success ? ConsoleColor.Green : ConsoleColor.Red;
                
                Console.ForegroundColor = color;
                Console.WriteLine($"{status.PadRight(6)} {test.PadRight(20)} {message}");
                Console.ResetColor();
                
                if (success) passed++;
            }

            Console.WriteLine($"\nSummary: {passed}/{total} tests passed");
        }

        private CustomerDto EditCustomer(CustomerDto customer)
        {
            Console.WriteLine("Edit customer details (press Enter to keep current value):\n");

            customer.FirstName = GetEditedValue("First Name", customer.FirstName);
            customer.LastName = GetEditedValue("Last Name", customer.LastName);
            customer.Email = GetEditedValue("Email", customer.Email);
            customer.Phone = GetEditedValue("Phone", customer.Phone);
            customer.Address = GetEditedValue("Address", customer.Address);
            customer.City = GetEditedValue("City", customer.City);
            customer.State = GetEditedValue("State", customer.State);
            customer.ZipCode = GetEditedValue("Zip Code", customer.ZipCode);
            customer.Country = GetEditedValue("Country", customer.Country);

            return customer;
        }

        private ProductDto EditProduct(ProductDto product)
        {
            Console.WriteLine("Edit product details (press Enter to keep current value):\n");

            product.Name = GetEditedValue("Name", product.Name);
            product.Description = GetEditedValue("Description", product.Description);
            
            var priceStr = GetEditedValue("Price", product.Price.ToString());
            if (decimal.TryParse(priceStr, out var price))
                product.Price = price;

            product.Category = GetEditedValue("Category", product.Category);
            
            var stockStr = GetEditedValue("Stock Quantity", product.StockQuantity.ToString());
            if (int.TryParse(stockStr, out var stock))
                product.StockQuantity = stock;

            return product;
        }

        private string GetEditedValue(string fieldName, string currentValue)
        {
            Console.Write($"{fieldName} [{currentValue}]: ");
            var input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? currentValue : input;
        }

        private void DisplayCustomer(CustomerDto customer)
        {
            Console.WriteLine($"ID: {customer.CustomerId}");
            Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone: {customer.Phone}");
            Console.WriteLine($"Address: {customer.Address}");
            Console.WriteLine($"City: {customer.City}, {customer.State} {customer.ZipCode}");
            Console.WriteLine($"Country: {customer.Country}");
            Console.WriteLine($"Active: {customer.IsActive}");
            Console.WriteLine($"Created: {customer.CreatedDate:yyyy-MM-dd HH:mm:ss}");
        }

        private void DisplayProduct(ProductDto product)
        {
            Console.WriteLine($"ID: {product.ProductId}");
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine($"Price: ${product.Price:F2}");
            Console.WriteLine($"Category: {product.Category}");
            Console.WriteLine($"Stock: {product.StockQuantity}");
            Console.WriteLine($"Active: {product.IsActive}");
            Console.WriteLine($"Created: {product.CreatedDate:yyyy-MM-dd HH:mm:ss}");
        }
    }
}
