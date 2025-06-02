# Sample Ecommerce Store API - Legacy .NET 4.5

A complete legacy .NET 4.5 ecommerce backend solution using WCF, Entity Framework, SQLite, Castle Windsor, and Microsoft Enterprise Library - representing the technology stack circa 2010-2012.

## Technology Stack

- **.NET Framework 4.5** - Core framework
- **WCF (Windows Communication Foundation)** - Service layer
- **Entity Framework 6.0** - ORM for data access
- **SQLite** - Database
- **Castle Windsor** - Dependency Injection/IoC container
- **Microsoft Enterprise Library** - Logging and validation
- **NUnit** - Unit testing framework
- **Microsoft Moles/Fakes** - Mocking framework
- **IIS Express** - Development web server

## Project Structure

```
SampleEcomStoreApi/
├── src/
│   ├── SampleEcomStoreApi.Contracts/        # Service and Data Contracts
│   ├── SampleEcomStoreApi.Common/           # Shared utilities, logging, validation
│   ├── SampleEcomStoreApi.DataAccess/       # Entity Framework models and repositories
│   ├── SampleEcomStoreApi.BusinessLogic/    # Business managers and rules
│   ├── SampleEcomStoreApi.Services/         # WCF service implementations
│   ├── SampleEcomStoreApi.ConsoleHost/      # Console-based WCF service host
│   ├── SampleEcomStoreApi.Host/             # WCF host with IIS Express
│   └── SampleEcomStoreApi.Client/           # Sample client application
├── tests/
│   ├── SampleEcomStoreApi.Tests/            # Unit tests
│   └── SampleEcomStoreApi.IntegrationTests/ # Integration tests
├── SampleEcomStoreApiTestClient/            # Modern .NET 8.0 Test Client (Separate Solution)
│   ├── TestClient/                          # Console-based test application
│   │   ├── ServiceProxies/                  # WCF service contracts & DTOs
│   │   ├── Services/                        # Service client factory
│   │   ├── Models/                          # Sample data templates
│   │   ├── UI/                              # Interactive console interface
│   │   └── Program.cs                       # Entry point
│   ├── README.md                            # Test client documentation
│   ├── GETTING-STARTED.md                  # Quick start guide
│   └── SampleEcomStoreApiTestClient.sln     # Separate test client solution
└── SampleEcomStoreApi.sln                   # Main API solution file
```

## Features

### Core Ecommerce Functionality
- **Product Management** - CRUD operations, search, categories
- **Customer Management** - Registration, profile management
- **Order Processing** - Cart, checkout, order tracking
- **Inventory Management** - Stock tracking and updates

### Technical Features
- **WCF Services** - RESTful and SOAP endpoints
- **Entity Framework** - Code-first with SQLite database
- **Dependency Injection** - Castle Windsor container
- **Enterprise Logging** - Microsoft Enterprise Library
- **Validation** - Enterprise Library validation
- **Unit Testing** - NUnit with AAA pattern
- **Mocking** - Microsoft Moles/Fakes

## Setup Instructions

### Prerequisites
- Visual Studio 2012 or later (VS 2010 SP1 minimum)
- .NET Framework 4.5
- IIS Express
- NuGet Package Manager

### Database Setup
The SQLite database will be created automatically in the `App_Data` folder when the application first runs.

### Running the Application

#### Option 1: Console Host (Recommended for Testing)

1. **Build the Solution**
   ```bash
   dotnet build SampleEcomStoreApi.sln
   # or use MSBuild: msbuild SampleEcomStoreApi.sln
   ```

2. **Run the Console Host**
   ```bash
   cd src/SampleEcomStoreApi.ConsoleHost/bin/Debug
   SampleEcomStoreApi.ConsoleHost.exe
   ```

3. **Services will be available at:**
   - Customer Service: `http://localhost:8732/CustomerService/`
   - Product Service: `http://localhost:8731/ProductService/`
   - Order Service: `http://localhost:8733/OrderService/`

#### Option 2: IIS Express (Legacy)

1. **Open the Solution**
   ```
   Open SampleEcomStoreApi.sln in Visual Studio
   ```

2. **Restore NuGet Packages**
   ```
   Right-click solution → Restore NuGet Packages
   ```

3. **Build the Solution**
   ```
   Build → Build Solution (Ctrl+Shift+B)
   ```

4. **Set Startup Project**
   ```
   Right-click SampleEcomStoreApi.Host → Set as StartUp Project
   ```

5. **Run the Application**
   ```
   Press F5 or Debug → Start Debugging
   ```

6. **Test with WCF Test Client**
   ```
   Open Visual Studio Command Prompt
   Navigate to: %ProgramFiles(x86)%\Microsoft Visual Studio 11.0\Common7\IDE
   Run: WcfTestClient.exe
   Add Service: http://localhost:8080/ProductService.svc
   ```

### Manual Build (MSBuild)
```bash
# From the solution directory
msbuild SampleEcomStoreApi.sln /p:Configuration=Debug
```

### Running Tests
```bash
# Unit Tests
nunit-console tests\SampleEcomStoreApi.Tests\bin\Debug\SampleEcomStoreApi.Tests.dll

# Integration Tests  
nunit-console tests\SampleEcomStoreApi.IntegrationTests\bin\Debug\SampleEcomStoreApi.IntegrationTests.dll
```

## Service Endpoints

### Product Service
- **Console Host**: `http://localhost:8731/ProductService/`
- **IIS Express**: `http://localhost:8080/ProductService.svc`
- **Operations**: GetAllProducts, GetProductById, CreateProduct, UpdateProduct, DeleteProduct

### Customer Service  
- **Console Host**: `http://localhost:8732/CustomerService/`
- **IIS Express**: `http://localhost:8080/CustomerService.svc`
- **Operations**: GetAllCustomers, GetCustomerById, CreateCustomer, UpdateCustomer

### Order Service
- **Console Host**: `http://localhost:8733/OrderService/`
- **IIS Express**: `http://localhost:8080/OrderService.svc`  
- **Operations**: GetAllOrders, GetOrderById, CreateOrder, UpdateOrderStatus

## Modern Test Client (.NET 8.0)

A comprehensive test client is included in the `SampleEcomStoreApiTestClient/` directory, built with modern .NET 8.0. This client is **perfect for API testing and upgrade verification**.

### Key Features

- **🎯 Pre-filled Sample Data** - No more typing boilerplate data repeatedly
- **📝 Interactive Forms** - Edit any field or press Enter to keep defaults
- **🔄 Comprehensive Testing** - Built-in regression test suite
- **🚀 Fast Build & Run** - Modern .NET 8.0 performance
- **📊 Professional UI** - Clean console interface with detailed reporting
- **🔗 WCF Integration** - Strongly-typed service proxies

### Quick Start

1. **Start the WCF Services**
   ```bash
   # From the main API directory
   cd src/SampleEcomStoreApi.ConsoleHost/bin/Debug
   SampleEcomStoreApi.ConsoleHost.exe
   ```

2. **Run the Test Client**
   ```bash
   # Navigate to test client (separate solution)
   cd SampleEcomStoreApiTestClient
   dotnet run --project TestClient
   ```

3. **Or use convenience scripts:**
   ```bash
   # Windows batch file
   build-and-run.bat
   
   # PowerShell script  
   .\quick-test-fixed.ps1 -Interactive
   ```

### Test Client Menu Options

```
1. View All Customers           # Browse existing customers
2. Create New Customer          # Pre-filled form, easy to customize
3. View All Products            # Browse existing products  
4. Create New Product           # Pre-filled form, easy to customize
5. Search Customer by Email     # Find specific customers
6. Test All Services            # Comprehensive regression testing
Q. Quit
```

### Perfect for API Upgrades

**Before Upgrade:**
- Run Option 6 (regression tests) to establish baseline
- Create test customers/products to verify functionality
- Document current behavior

**After Upgrade:**  
- Run the same tests to ensure nothing broke
- Verify all endpoints still work correctly
- Test new features interactively

### Sample Data Templates

The test client includes realistic pre-filled data to speed up testing:

**Customer Template:**
```
Name: John Smith
Email: john.smith@example.com  
Phone: 555-123-4567
Address: 123 Main Street, New York, NY 10001, USA
```

**Product Template:**
```
Name: Sample Laptop
Description: High-performance laptop for testing
Price: $1299.99
Category: Electronics
Stock: 10 units
```

### Regression Testing

Option 6 provides comprehensive automated testing:
- ✅ Tests all major service endpoints
- ✅ Creates unique test data (no conflicts)
- ✅ Validates service responses
- ✅ Provides detailed pass/fail reporting
- ✅ Exports results to CSV for documentation
- ✅ Perfect for CI/CD integration

### Architecture Benefits

- **Separate Solution** - Won't be affected by API changes
- **Modern .NET 8.0** - Fast compilation and execution
- **Strongly-typed Proxies** - Compile-time safety and IntelliSense
- **Modular Design** - Easy to extend with new test scenarios

## Sample Data

The main API includes seeded sample data:

### Products
- Electronics (Laptops, Smartphones, Tablets)
- Books (Fiction, Technical, Business)
- Clothing (Shirts, Pants, Shoes)

### Customers
- John Doe (john.doe@email.com)
- Jane Smith (jane.smith@email.com) 
- Bob Johnson (bob.johnson@email.com)

### Orders
- Sample orders with various statuses (Pending, Processing, Shipped, Delivered)

## Architecture Patterns

### Repository Pattern
- `IProductRepository`, `ICustomerRepository`, `IOrderRepository`
- Data access abstraction layer

### Manager Pattern  
- `ProductManager`, `CustomerManager`, `OrderManager`
- Business logic encapsulation

### Data Transfer Objects (DTOs)
- `ProductDto`, `CustomerDto`, `OrderDto`
- Service contract data models

### Dependency Injection
- Castle Windsor container configuration
- Interface-based design for testability

## Testing Strategy

### Unit Tests (AAA Pattern)
```csharp
[Test]
public void CreateProduct_ValidProduct_ReturnsProductId()
{
    // Arrange
    var productManager = new ProductManager(mockRepository);
    var product = new ProductDto { Name = "Test Product" };
    
    // Act  
    var result = productManager.CreateProduct(product);
    
    // Assert
    Assert.IsTrue(result > 0);
}
```

### Integration Tests
- WCF service endpoint testing
- Database integration testing
- End-to-end workflow testing

## Legacy Technology Notes

This solution represents the typical enterprise .NET stack from 2010-2012:

- **WCF** was the preferred service technology before Web API
- **Enterprise Library** was Microsoft's recommended practice framework
- **Castle Windsor** was a popular IoC container choice
- **Entity Framework 6.0** was the mature ORM option
- **Moles/Fakes** were Microsoft's mocking tools before Moq gained popularity
- **NUnit** was widely used for unit testing

## Troubleshooting

### Common Issues

1. **NuGet Package Restore Fails**
   - Enable NuGet Package Restore in Visual Studio
   - Clear NuGet cache: `nuget locals all -clear`

2. **WCF Service Doesn't Start**
   - Check IIS Express is installed
   - Verify ports 8731-8733 (Console Host) or 8080 (IIS Express) are available
   - Check Windows Firewall settings
   - Run `netstat -an | findstr 8732` to verify services are listening

3. **Database Connection Issues**
   - Ensure SQLite provider is properly installed
   - Check App_Data folder permissions
   - Verify connection string in web.config
   - For LocalDB: Check SQL Server LocalDB is installed and running

4. **Castle Windsor Registration Errors**
   - Check all dependencies are properly registered
   - Verify interface implementations exist
   - Review container configuration

### Test Client Issues

1. **"Could not connect to service" in Test Client**
   - Ensure WCF services are running first: `SampleEcomStoreApi.ConsoleHost.exe`
   - Check services are listening: `netstat -an | findstr 8732`
   - Verify firewall is not blocking connections

2. **Test Client Build Errors**
   - Ensure .NET 8.0 SDK is installed
   - Run `dotnet --version` to verify
   - Clean and rebuild: `dotnet clean && dotnet build`

3. **Service Contract Mismatch**
   - API contracts may have changed after upgrade
   - Update service proxies in `TestClient/ServiceProxies/` if needed
   - Check namespace and contract attributes match WCF service

## Additional Notes

- The solution targets .NET Framework 4.5 for maximum compatibility with legacy systems
- All projects use the classic .csproj format (not SDK-style)
- Package references use packages.config (not PackageReference)
- Assembly binding redirects may be needed for dependency resolution
- The solution can be opened and built in Visual Studio 2012-2022

For questions or issues, refer to the individual project README files or check the inline code documentation.
