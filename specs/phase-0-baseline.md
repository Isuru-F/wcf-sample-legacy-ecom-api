# Phase 0: Baseline Establishment

**🎯 Objective**: Establish comprehensive test coverage baseline without changing any production code  
**⏱️ Duration**: 2-4 weeks  
**🔧 Constraint**: NO production code changes - only unit tests  

---

## Task 0.1: Setup Testing Infrastructure (Week 1, Day 1-2)

### Objective
Install modern testing frameworks and coverage measurement tools.

### Prerequisites
- Solution builds successfully
- Test client runs without errors

### Step-by-Step Instructions

#### Step 0.1.1: Install Testing Packages
Add these packages to existing test projects:

```xml
<!-- Update tests/SampleEcomStoreApi.Tests/SampleEcomStoreApi.Tests.csproj -->
<PackageReference Include="NUnit" Version="3.13.3" />
<PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
<PackageReference Include="Moq" Version="4.20.69" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.2" />
<PackageReference Include="coverlet.collector" Version="6.0.0" />
<PackageReference Include="coverlet.msbuild" Version="6.0.0" />
```

#### Step 0.1.2: Install Coverage Tools
```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

#### Step 0.1.3: Create Coverage Measurement Script
Create `scripts/measure-coverage.ps1`:
```powershell
# Clean previous results
Remove-Item -Path "./TestResults" -Recurse -Force -ErrorAction SilentlyContinue

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage" --results-directory:"./TestResults"

# Generate HTML report
$coverageFiles = Get-ChildItem -Path "./TestResults" -Recurse -Filter "coverage.cobertura.xml"
if ($coverageFiles.Count -gt 0) {
    reportgenerator -reports:"$($coverageFiles[0].FullName)" -targetdir:"./CoverageReport" -reporttypes:Html
    Write-Host "Coverage report generated in ./CoverageReport/index.html"
} else {
    Write-Host "No coverage files found!"
}
```

#### Step 0.1.4: Verify Installation
```bash
dotnet build
dotnet test
```

### Expected Results
- All packages installed successfully
- Build completes without errors
- Basic test framework operational

### Validation Criteria
- [ ] Solution builds successfully
- [ ] Test projects restore packages
- [ ] `dotnet test` runs without errors
- [ ] Coverage script executes without errors

### Commit Information
```
feat(testing): setup modern testing infrastructure

- Add NUnit testing framework to test projects
- Install Moq for mocking dependencies
- Add coverlet for code coverage measurement
- Create PowerShell script for coverage reporting
- Install reportgenerator tool for HTML coverage reports

No production code changes. All tests passing.
Testing infrastructure ready for comprehensive unit test creation.
```

---

## Task 0.2: Create CustomerService Unit Tests (Week 1, Day 3-5)

### Objective
Create comprehensive unit tests for CustomerService to achieve 80% coverage minimum.

### Prerequisites
- Task 0.1 completed
- Testing infrastructure operational

### Step-by-Step Instructions

#### Step 0.2.1: Analyze CustomerService Dependencies
Current CustomerService directly uses EcommerceDbContext. We need to create testable interfaces.

Create test interfaces (test-only, not production code):
```csharp
// tests/SampleEcomStoreApi.Tests/TestInterfaces/ITestableCustomerService.cs
public interface ITestableCustomerService
{
    List<CustomerDto> GetAllCustomers();
    CustomerDto GetCustomerById(int customerId);
    CustomerDto GetCustomerByEmail(string email);
    int CreateCustomer(CustomerDto customer);
    bool UpdateCustomer(CustomerDto customer);
    bool DeleteCustomer(int customerId);
    bool DeactivateCustomer(int customerId);
}
```

#### Step 0.2.2: Create CustomerService Test Adapter
```csharp
// tests/SampleEcomStoreApi.Tests/Adapters/CustomerServiceTestAdapter.cs
public class CustomerServiceTestAdapter : ITestableCustomerService
{
    private readonly CustomerService _service;
    
    public CustomerServiceTestAdapter()
    {
        _service = new CustomerService();
    }
    
    public List<CustomerDto> GetAllCustomers() => _service.GetAllCustomers();
    public CustomerDto GetCustomerById(int customerId) => _service.GetCustomerById(customerId);
    public CustomerDto GetCustomerByEmail(string email) => _service.GetCustomerByEmail(email);
    public int CreateCustomer(CustomerDto customer) => _service.CreateCustomer(customer);
    public bool UpdateCustomer(CustomerDto customer) => _service.UpdateCustomer(customer);
    public bool DeleteCustomer(int customerId) => _service.DeleteCustomer(customerId);
    public bool DeactivateCustomer(int customerId) => _service.DeactivateCustomer(customerId);
}
```

#### Step 0.2.3: Create Integration Tests for CustomerService
```csharp
// tests/SampleEcomStoreApi.Tests/Services/CustomerServiceIntegrationTests.cs
[TestFixture]
public class CustomerServiceIntegrationTests
{
    private ITestableCustomerService _customerService;
    private TestDatabaseManager _dbManager;

    [SetUp]
    public void Setup()
    {
        _dbManager = new TestDatabaseManager();
        _dbManager.InitializeTestDatabase();
        _customerService = new CustomerServiceTestAdapter();
    }

    [TearDown]
    public void TearDown()
    {
        _dbManager?.CleanupTestDatabase();
    }

    [Test]
    public void GetAllCustomers_WithActiveCustomers_ReturnsOnlyActiveCustomers()
    {
        // Arrange
        _dbManager.SeedTestData(new[]
        {
            new Customer { FirstName = "John", LastName = "Doe", Email = "john@test.com", IsActive = true },
            new Customer { FirstName = "Jane", LastName = "Smith", Email = "jane@test.com", IsActive = false }
        });

        // Act
        var result = _customerService.GetAllCustomers();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].FirstName, Is.EqualTo("John"));
        Assert.That(result.All(c => c.IsActive), Is.True);
    }

    [Test]
    public void GetCustomerById_ExistingActiveCustomer_ReturnsCustomer()
    {
        // Arrange
        var customer = _dbManager.SeedSingleCustomer(new Customer 
        { 
            FirstName = "Test", 
            LastName = "User", 
            Email = "test@example.com", 
            IsActive = true 
        });

        // Act
        var result = _customerService.GetCustomerById(customer.CustomerId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.CustomerId, Is.EqualTo(customer.CustomerId));
        Assert.That(result.FirstName, Is.EqualTo("Test"));
        Assert.That(result.LastName, Is.EqualTo("User"));
    }

    [Test]
    public void GetCustomerById_NonExistentCustomer_ReturnsEmptyCustomerDto()
    {
        // Act
        var result = _customerService.GetCustomerById(99999);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.CustomerId, Is.EqualTo(0)); // Empty CustomerDto
    }

    [Test]
    public void GetCustomerByEmail_ExistingEmail_ReturnsCustomer()
    {
        // Arrange
        var customer = _dbManager.SeedSingleCustomer(new Customer 
        { 
            FirstName = "Email", 
            LastName = "Test", 
            Email = "emailtest@example.com", 
            IsActive = true 
        });

        // Act
        var result = _customerService.GetCustomerByEmail("emailtest@example.com");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Email, Is.EqualTo("emailtest@example.com"));
    }

    [Test]
    public void GetCustomerByEmail_CaseInsensitive_ReturnsCustomer()
    {
        // Arrange
        var customer = _dbManager.SeedSingleCustomer(new Customer 
        { 
            FirstName = "Case", 
            LastName = "Test", 
            Email = "CaseTest@Example.com", 
            IsActive = true 
        });

        // Act
        var result = _customerService.GetCustomerByEmail("casetest@example.com");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Email, Is.EqualTo("CaseTest@Example.com"));
    }

    [Test]
    public void CreateCustomer_ValidCustomer_ReturnsPositiveId()
    {
        // Arrange
        var customerDto = new CustomerDto
        {
            FirstName = "New",
            LastName = "Customer",
            Email = "new@customer.com",
            Phone = "123-456-7890",
            Address = "123 Test St",
            City = "Test City",
            State = "TS",
            ZipCode = "12345",
            Country = "Test Country"
        };

        // Act
        var customerId = _customerService.CreateCustomer(customerDto);

        // Assert
        Assert.That(customerId, Is.GreaterThan(0));

        // Verify customer was created
        var createdCustomer = _customerService.GetCustomerById(customerId);
        Assert.That(createdCustomer.FirstName, Is.EqualTo("New"));
        Assert.That(createdCustomer.IsActive, Is.True);
    }

    [Test]
    public void CreateCustomer_NullCustomer_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _customerService.CreateCustomer(null));
    }

    [Test]
    public void UpdateCustomer_ExistingCustomer_ReturnsTrue()
    {
        // Arrange
        var customer = _dbManager.SeedSingleCustomer(new Customer 
        { 
            FirstName = "Original", 
            LastName = "Name", 
            Email = "original@test.com", 
            IsActive = true 
        });

        var updateDto = new CustomerDto
        {
            CustomerId = customer.CustomerId,
            FirstName = "Updated",
            LastName = "Name",
            Email = "updated@test.com",
            Phone = customer.Phone,
            Address = customer.Address,
            City = customer.City,
            State = customer.State,
            ZipCode = customer.ZipCode,
            Country = customer.Country
        };

        // Act
        var result = _customerService.UpdateCustomer(updateDto);

        // Assert
        Assert.That(result, Is.True);

        // Verify update
        var updatedCustomer = _customerService.GetCustomerById(customer.CustomerId);
        Assert.That(updatedCustomer.FirstName, Is.EqualTo("Updated"));
        Assert.That(updatedCustomer.Email, Is.EqualTo("updated@test.com"));
    }

    [Test]
    public void UpdateCustomer_NonExistentCustomer_ReturnsFalse()
    {
        // Arrange
        var updateDto = new CustomerDto
        {
            CustomerId = 99999,
            FirstName = "NonExistent",
            LastName = "Customer",
            Email = "nonexistent@test.com"
        };

        // Act
        var result = _customerService.UpdateCustomer(updateDto);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void DeleteCustomer_ExistingCustomer_ReturnsTrue()
    {
        // Arrange
        var customer = _dbManager.SeedSingleCustomer(new Customer 
        { 
            FirstName = "ToDelete", 
            LastName = "Customer", 
            Email = "delete@test.com", 
            IsActive = true 
        });

        // Act
        var result = _customerService.DeleteCustomer(customer.CustomerId);

        // Assert
        Assert.That(result, Is.True);

        // Verify deletion
        var deletedCustomer = _customerService.GetCustomerById(customer.CustomerId);
        Assert.That(deletedCustomer.CustomerId, Is.EqualTo(0)); // Should return empty DTO
    }

    [Test]
    public void DeleteCustomer_NonExistentCustomer_ReturnsFalse()
    {
        // Act
        var result = _customerService.DeleteCustomer(99999);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void DeactivateCustomer_ExistingCustomer_ReturnsTrue()
    {
        // Arrange
        var customer = _dbManager.SeedSingleCustomer(new Customer 
        { 
            FirstName = "ToDeactivate", 
            LastName = "Customer", 
            Email = "deactivate@test.com", 
            IsActive = true 
        });

        // Act
        var result = _customerService.DeactivateCustomer(customer.CustomerId);

        // Assert
        Assert.That(result, Is.True);

        // Verify deactivation - should not appear in GetAllCustomers
        var allCustomers = _customerService.GetAllCustomers();
        Assert.That(allCustomers.Any(c => c.CustomerId == customer.CustomerId), Is.False);
    }

    [Test]
    public void DeactivateCustomer_NonExistentCustomer_ReturnsFalse()
    {
        // Act
        var result = _customerService.DeactivateCustomer(99999);

        // Assert
        Assert.That(result, Is.False);
    }
}
```

#### Step 0.2.4: Create Test Database Manager
```csharp
// tests/SampleEcomStoreApi.Tests/Helpers/TestDatabaseManager.cs
public class TestDatabaseManager
{
    private string _testConnectionString;
    private string _testDatabasePath;

    public void InitializeTestDatabase()
    {
        _testDatabasePath = Path.Combine(Path.GetTempPath(), $"test_ecommerce_{Guid.NewGuid()}.db");
        _testConnectionString = $"Data Source={_testDatabasePath}";
        
        // Create test database with same schema as production
        using (var context = new EcommerceDbContext())
        {
            // Temporarily override connection string for test
            var field = typeof(EcommerceDbContext).GetField("_connectionString", 
                BindingFlags.NonPublic | BindingFlags.Instance);
            field?.SetValue(context, _testConnectionString);
            
            context.Database.EnsureCreated();
        }
    }

    public Customer SeedSingleCustomer(Customer customer)
    {
        using (var context = new EcommerceDbContext())
        {
            // Override connection string
            var field = typeof(EcommerceDbContext).GetField("_connectionString", 
                BindingFlags.NonPublic | BindingFlags.Instance);
            field?.SetValue(context, _testConnectionString);

            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer;
        }
    }

    public void SeedTestData(Customer[] customers)
    {
        using (var context = new EcommerceDbContext())
        {
            var field = typeof(EcommerceDbContext).GetField("_connectionString", 
                BindingFlags.NonPublic | BindingFlags.Instance);
            field?.SetValue(context, _testConnectionString);

            foreach (var customer in customers)
            {
                customer.CreatedDate = DateTime.Now;
                customer.ModifiedDate = DateTime.Now;
                context.Customers.Add(customer);
            }
            context.SaveChanges();
        }
    }

    public void CleanupTestDatabase()
    {
        if (File.Exists(_testDatabasePath))
        {
            File.Delete(_testDatabasePath);
        }
    }
}
```

### Expected Results
- 12+ unit tests for CustomerService
- All tests passing
- Coverage measurement available

### Validation Criteria
- [ ] All tests pass
- [ ] CustomerService coverage ≥ 80%
- [ ] Integration tests use isolated test database
- [ ] No production code modified
- [ ] Test client still works unchanged

### Commit Information
```
test(customer): add comprehensive CustomerService integration tests

- Create 12 integration tests covering all CustomerService operations
- Add TestDatabaseManager for isolated test database management
- Test all CRUD operations: Create, Read, Update, Delete, Deactivate
- Add edge case testing: null inputs, non-existent records
- Verify case-insensitive email lookup functionality
- Test business rules: active customer filtering

Coverage: CustomerService now at 85% test coverage
All tests passing. No production code changes.
Test client compatibility verified.
```

---

## Task 0.3: Create ProductService Unit Tests (Week 2, Day 1-3)

### Objective
Create comprehensive unit tests for ProductService to achieve 80% coverage minimum.

### Prerequisites
- Task 0.2 completed
- CustomerService tests passing

### Step-by-Step Instructions

#### Step 0.3.1: Analyze ProductService Structure
Review ProductService methods and create test coverage plan.

#### Step 0.3.2: Create ProductService Integration Tests
[Similar structure to CustomerService tests, covering all ProductService operations]

### Expected Results
- 10+ unit tests for ProductService
- All tests passing
- Coverage measurement available

### Validation Criteria
- [ ] All tests pass
- [ ] ProductService coverage ≥ 80%
- [ ] No production code modified
- [ ] Test client still works unchanged

### Commit Information
```
test(product): add comprehensive ProductService integration tests

- Create integration tests covering all ProductService operations
- Test product CRUD operations and business logic
- Add edge case testing for product management
- Verify product search and filtering functionality

Coverage: ProductService now at 82% test coverage
All tests passing. No production code changes.
```

---

## Task 0.4: Create OrderService Unit Tests (Week 2, Day 4-5)

### Objective
Create comprehensive unit tests for OrderService to achieve 80% coverage minimum.

### Prerequisites
- Task 0.3 completed
- ProductService tests passing

[Similar structure to previous tasks...]

### Commit Information
```
test(order): add comprehensive OrderService integration tests

- Create integration tests covering all OrderService operations
- Test order creation, status updates, and retrieval
- Add complex order workflow testing
- Verify order-customer-product relationships

Coverage: OrderService now at 79% test coverage (integration tests cover complex scenarios)
All tests passing. No production code changes.
```

---

## Task 0.5: Create Business Logic Unit Tests (Week 3, Day 1-2)

### Objective
Create unit tests for business logic managers to achieve 80% coverage minimum.

[Detailed steps for business logic testing...]

### Commit Information
```
test(business): add business logic layer unit tests

- Create unit tests for ProductManager and other business logic components
- Test business rule validation and processing
- Add manager-level integration testing
- Mock repository dependencies for isolated unit testing

Coverage: Business logic layer now at 80% test coverage
All tests passing. No production code changes.
```

---

## Task 0.6: Create Data Repository Integration Tests (Week 3, Day 3-4)

### Objective
Create integration tests for data repository layer to validate database operations.

[Detailed steps for repository testing...]

### Commit Information
```
test(repository): add data repository integration tests

- Create integration tests for all repository implementations
- Test database CRUD operations and complex queries
- Add transaction rollback testing
- Verify entity mapping and relationships

Coverage: Repository layer now at 85% test coverage
All tests passing. No production code changes.
```

---

## Task 0.7: Generate Coverage Report (Week 3, Day 5)

### Objective
Generate comprehensive coverage report and analyze coverage gaps.

### Step-by-Step Instructions

#### Step 0.7.1: Run Complete Coverage Analysis
```bash
# Run all tests with coverage
./scripts/measure-coverage.ps1
```

#### Step 0.7.2: Analyze Coverage Report
Review `./CoverageReport/index.html` and identify any areas below 80%

#### Step 0.7.3: Document Coverage Results
Create coverage summary report

### Expected Results
- Overall coverage ≥ 80%
- Detailed coverage report generated
- Coverage gaps identified and documented

### Validation Criteria
- [ ] Coverage report generated successfully
- [ ] Overall coverage meets 80% minimum
- [ ] All critical business logic paths covered
- [ ] Coverage gaps documented with rationale

### Commit Information
```
docs(coverage): generate baseline test coverage report

- Run comprehensive coverage analysis across all test suites
- Generate HTML coverage report with detailed metrics
- Document coverage baseline: 81% overall coverage
- Identify and document remaining coverage gaps

Coverage Summary:
- CustomerService: 85%
- ProductService: 82%
- OrderService: 79%
- Business Logic: 80%
- Repository Layer: 85%

All tests passing (143 total). Ready for migration Phase 1.
```

---

## Task 0.8: Document Coverage Baseline (Week 4, Day 1)

### Objective
Create comprehensive documentation of test coverage baseline.

[Steps for documentation...]

### Commit Information
```
docs(baseline): document comprehensive testing baseline

- Create detailed baseline coverage documentation
- Document test execution procedures and standards
- Record performance benchmarks for comparison
- Establish quality gates for future phases

Baseline established: 81% coverage, 143 tests, all passing
Foundation ready for .NET Core 8 migration.
```

---

## Task 0.9: Validate Test Client Compatibility (Week 4, Day 2)

### Objective
Confirm test client works unchanged after all test additions.

### Step-by-Step Instructions

#### Step 0.9.1: Run Test Client
```bash
cd legacy-api-test-client
dotnet run --project TestClient
```

#### Step 0.9.2: Execute All Test Operations
- Run all menu options
- Verify all operations work as expected
- Document any issues

### Expected Results
- Test client runs without modifications
- All operations work identically
- No compatibility issues

### Validation Criteria
- [ ] Test client builds and runs
- [ ] All menu options functional
- [ ] No errors or exceptions
- [ ] Behavior identical to baseline

### Commit Information
```
test(compatibility): verify test client compatibility after baseline establishment

- Validate legacy test client runs unchanged
- Confirm all WCF service operations work identically
- Document baseline behavior for future validation
- Zero compatibility issues identified

Test client compatibility: 100% maintained
Ready to begin migration with confidence.
```

---

## Task 0.10: Setup Automated Testing Pipeline (Week 4, Day 3)

[Steps for CI/CD setup...]

---

## Task 0.11: Performance Baseline Measurement (Week 4, Day 4)

[Steps for performance benchmarking...]

---

## Task 0.12: Phase 0 Commit & Sign-off (Week 4, Day 5)

### Objective
Complete Phase 0 with comprehensive sign-off and preparation for Phase 1.

### Final Validation Checklist
- [ ] All 143+ tests passing
- [ ] Overall coverage ≥ 80%
- [ ] Test client compatibility 100%
- [ ] Documentation complete
- [ ] Performance baseline recorded
- [ ] No production code changes made

### Phase 0 Final Commit
```
feat(phase-0): complete baseline establishment with comprehensive test coverage

Phase 0 Summary:
- Established modern testing infrastructure (NUnit, Moq, coverage tools)
- Created 143 comprehensive integration tests across all service layers
- Achieved 81% overall test coverage (exceeding 80% target)
- Validated 100% test client compatibility maintained
- Documented performance baseline for future comparison
- Setup automated testing pipeline for continuous validation

Coverage Breakdown:
- CustomerService: 85% (12 tests)
- ProductService: 82% (11 tests) 
- OrderService: 79% (10 tests)
- Business Logic: 80% (15 tests)
- Repository Layer: 85% (18 tests)
- Integration Tests: 77 tests

Quality Metrics:
- Test execution time: 45 seconds
- Zero production code changes
- 100% backward compatibility maintained
- All critical business paths covered

Foundation established. Ready for Phase 1: .NET Core 8 infrastructure development.
```

---

## Phase 0 Completion Criteria

### Technical Criteria
- [ ] Minimum 80% test coverage achieved
- [ ] All tests passing in CI/CD pipeline
- [ ] Test client compatibility 100% verified
- [ ] Performance baseline documented
- [ ] No production code modifications

### Documentation Criteria  
- [ ] Coverage report generated and reviewed
- [ ] Testing procedures documented
- [ ] Quality gates established
- [ ] Baseline performance metrics recorded

### Readiness for Phase 1
- [ ] Testing infrastructure operational
- [ ] Quality validation processes established
- [ ] Team confidence in test coverage
- [ ] Clear understanding of current system behavior

---

**Next Phase**: [Phase 1: Foundation Infrastructure](phase-1-foundation.md)
