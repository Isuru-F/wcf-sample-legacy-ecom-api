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
│   ├── SampleEcomStoreApi.Contracts/       # Service and Data Contracts
│   ├── SampleEcomStoreApi.Common/          # Shared utilities, logging, validation
│   ├── SampleEcomStoreApi.DataAccess/      # Entity Framework models and repositories
│   ├── SampleEcomStoreApi.BusinessLogic/   # Business managers and rules
│   ├── SampleEcomStoreApi.Services/        # WCF service implementations
│   ├── SampleEcomStoreApi.Host/             # WCF host with IIS Express
│   └── SampleEcomStoreApi.Client/           # Sample client application
├── tests/
│   ├── SampleEcomStoreApi.Tests/            # Unit tests
│   └── SampleEcomStoreApi.IntegrationTests/ # Integration tests
└── SampleEcomStoreApi.sln                  # Solution file
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
- **Base URL**: `http://localhost:8080/ProductService.svc`
- **Operations**: GetAllProducts, GetProductById, CreateProduct, UpdateProduct, DeleteProduct

### Customer Service  
- **Base URL**: `http://localhost:8080/CustomerService.svc`
- **Operations**: GetAllCustomers, GetCustomerById, CreateCustomer, UpdateCustomer

### Order Service
- **Base URL**: `http://localhost:8080/OrderService.svc`  
- **Operations**: GetAllOrders, GetOrderById, CreateOrder, UpdateOrderStatus

## Sample Data

The application includes seeded sample data:

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
   - Verify port 8080 is available
   - Check Windows Firewall settings

3. **Database Connection Issues**
   - Ensure SQLite provider is properly installed
   - Check App_Data folder permissions
   - Verify connection string in web.config

4. **Castle Windsor Registration Errors**
   - Check all dependencies are properly registered
   - Verify interface implementations exist
   - Review container configuration

## Additional Notes

- The solution targets .NET Framework 4.5 for maximum compatibility with legacy systems
- All projects use the classic .csproj format (not SDK-style)
- Package references use packages.config (not PackageReference)
- Assembly binding redirects may be needed for dependency resolution
- The solution can be opened and built in Visual Studio 2012-2022

For questions or issues, refer to the individual project README files or check the inline code documentation.
