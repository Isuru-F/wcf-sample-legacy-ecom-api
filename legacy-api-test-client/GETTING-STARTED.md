# Getting Started - Sample Ecommerce Store API Test Client

## Quick Start (2-Minute Setup)

### Step 1: Start the WCF Services
```bash
# In the main API directory
cd src/SampleEcomStoreApi.ConsoleHost/bin/Debug
SampleEcomStoreApi.ConsoleHost.exe
```

### Step 2: Run the Test Client
```bash
# In the test client directory
cd SampleEcomStoreApiTestClient
dotnet run --project TestClient
```

**OR** use the convenience scripts:
- Windows: `build-and-run.bat`
- PowerShell: `.\quick-test-fixed.ps1 -Interactive`

## What You Get

### 📋 Interactive Menu System
```
Main Menu:
1. View All Customers
2. Create New Customer      ← Pre-filled with sample data!
3. View All Products
4. Create New Product       ← Pre-filled with sample data!
5. Search Customer by Email
6. Test All Services        ← Perfect for regression testing!
Q. Quit
```

### 🎯 Pre-filled Sample Data
When creating customers or products, forms come pre-filled with realistic data:

**Customer Form:**
- John Smith, john.smith@example.com
- 555-123-4567, 123 Main Street, New York, NY 10001, USA

**Product Form:**
- Sample Laptop, High-performance laptop for testing
- $1299.99, Electronics category, 10 in stock

Just press Enter to accept defaults, or type new values to customize!

### 🧪 Built-in Regression Testing
Option 6 runs comprehensive tests perfect for API upgrades:
- Tests all major endpoints
- Creates unique test data
- Validates responses
- Provides detailed pass/fail reporting

## Perfect for API Upgrades

### Before Upgrade:
1. Run regression tests to establish baseline
2. Document current functionality

### After Upgrade:
1. Run the same tests to verify nothing broke
2. Test new features interactively
3. Generate reports for stakeholders

## Architecture Highlights

- **Modern .NET 8.0** - Fast build and startup
- **Strongly-typed WCF proxies** - Compile-time safety
- **Separation of concerns** - Clean, maintainable code
- **Independent solution** - Won't be affected by API changes
- **Zero external dependencies** - Just .NET and the WCF services

## Troubleshooting

**"Could not connect to service"**
- Ensure WCF services are running: `netstat -an | findstr 8732`
- Check firewall settings

**"Service contract mismatch"**
- API contracts may have changed
- Regenerate service proxies if needed

## Next Steps

1. Try creating a few customers and products
2. Run the regression test suite (option 6)
3. Customize the sample data in `Models/SampleData.cs`
4. Add new test scenarios as needed

This test client will be your best friend during WCF service upgrades! 🚀
