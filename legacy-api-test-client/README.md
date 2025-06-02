# Sample Ecommerce Store API Test Client

A comprehensive test client for the Sample Ecommerce Store WCF API, built with .NET 8.0.

## Features

- **Interactive Console Interface** - Easy-to-use menu system
- **Prefilled Sample Data** - Avoid typing boilerplate data every time
- **Full CRUD Operations** - Create, read, update, delete for customers and products
- **Regression Testing** - Built-in test suite to verify API functionality after upgrades
- **Service Proxy Generation** - Strongly-typed client proxies for WCF services

## Quick Start

### Prerequisites
- .NET 8.0 SDK
- Sample Ecommerce Store API running on localhost (ports 8731-8733)

### Build and Run

```bash
# Navigate to the test client directory
cd SampleEcomStoreApiTestClient

# Restore dependencies and build
dotnet build

# Run the test client
dotnet run --project TestClient
```

### Start the WCF Services First
Make sure the WCF services are running before using the test client:

```bash
# In the main API directory
cd src/SampleEcomStoreApi.ConsoleHost/bin/Debug
SampleEcomStoreApi.ConsoleHost.exe
```

## Usage

### Main Menu Options

1. **View All Customers** - Displays all customers from the database
2. **Create New Customer** - Create a customer with prefilled sample data that you can edit
3. **View All Products** - Displays all products from the database  
4. **Create New Product** - Create a product with prefilled sample data that you can edit
5. **Search Customer by Email** - Find a specific customer by email address
6. **Test All Services** - Run comprehensive regression tests on all endpoints

### Creating Customers/Products

When creating customers or products, the form will be prefilled with realistic sample data:

**Sample Customer Data:**
- Name: John Smith
- Email: john.smith@example.com
- Phone: 555-123-4567
- Address: 123 Main Street, New York, NY 10001, USA

**Sample Product Data:**
- Name: Sample Laptop
- Description: High-performance laptop for testing
- Price: $1299.99
- Category: Electronics
- Stock: 10 units

You can edit any field by typing new values, or press Enter to keep the prefilled values.

### Regression Testing

Option 6 runs a comprehensive test suite that:
- Tests all major service endpoints
- Creates test data with unique identifiers
- Validates responses
- Provides detailed pass/fail reporting
- Perfect for verifying functionality after API upgrades

## Architecture

### Project Structure
```
TestClient/
├── Models/
│   └── SampleData.cs          # Prefilled sample data
├── ServiceProxies/
│   ├── CustomerDto.cs         # Customer data contract
│   ├── ProductDto.cs          # Product data contract
│   ├── ICustomerService.cs    # Customer service contract
│   └── IProductService.cs     # Product service contract
├── Services/
│   └── ServiceClientFactory.cs # WCF client factory
├── UI/
│   └── ConsoleUI.cs           # Console interface
└── Program.cs                 # Entry point
```

### Service Endpoints
- **Customer Service**: http://localhost:8732/CustomerService/
- **Product Service**: http://localhost:8731/ProductService/
- **Order Service**: http://localhost:8733/OrderService/

## Benefits for API Upgrades

This test client is specifically designed to help with API upgrades:

1. **Pre-Upgrade Testing** - Run tests to establish baseline functionality
2. **Post-Upgrade Verification** - Run the same tests to ensure nothing broke
3. **Automated Regression Testing** - Option 6 provides comprehensive test coverage
4. **Quick Manual Testing** - Interactive interface for ad-hoc testing
5. **Separate Codebase** - Independent of the main API, won't be affected by API changes

## Extending the Client

To add new functionality:

1. **Add new DTOs** in `ServiceProxies/` folder
2. **Update service contracts** in `ServiceProxies/` folder  
3. **Add sample data** in `Models/SampleData.cs`
4. **Add UI methods** in `UI/ConsoleUI.cs`
5. **Update main menu** in `UI/ConsoleUI.cs`

## Troubleshooting

### Common Issues

**"Could not connect to service"**
- Ensure WCF services are running on localhost:8731-8733
- Check Windows Firewall settings
- Verify no other applications are using those ports

**"Service contract mismatch"**
- Regenerate service proxies if API contracts have changed
- Check namespace and contract attributes match the WCF service

**"Serialization errors"**
- Ensure DataContract attributes are properly set
- Check that all required fields are included in DTOs

### Debugging

Enable verbose logging by setting breakpoints in:
- `ServiceClientFactory.cs` - Service connection issues
- `ConsoleUI.cs` - UI and business logic issues
- Service proxy classes - Serialization issues
