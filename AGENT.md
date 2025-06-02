# SampleEcomStoreApi - Legacy .NET 4.5 WCF Service

## Build & Test Commands
- Build solution: `dotnet build SampleEcomStoreApi.sln` or `msbuild SampleEcomStoreApi.sln`
- Build specific project: `dotnet build src/[ProjectName]/[ProjectName].csproj`
- Run all tests: `dotnet test tests/SampleEcomStoreApi.Tests/SampleEcomStoreApi.Tests.csproj`
- Run specific test: tests are manual console execution via TestRunner.exe or inline test methods
- Full solution build: `.\Build-Solution.ps1` (PowerShell script)

## Technology Stack
- .NET Framework 4.5 (legacy)
- WCF services with DataContracts and ServiceContracts
- Entity Framework with SQLite/SQL Server
- Castle Windsor IoC container
- Enterprise Library for logging

## Code Style & Conventions
- **Namespaces**: Use full dotted naming (SampleEcomStoreApi.ProjectName.FolderName)
- **Classes**: PascalCase with descriptive names (CustomerService, ProductManager)
- **Properties**: Auto-properties with DataMember attributes for DTOs
- **Methods**: PascalCase verbs (GetCustomerById, CreateProduct)
- **Parameters**: camelCase (customerId, customerDto)
- **Using statements**: System first, then alphabetical third-party, then project references
- **Error handling**: Return null/empty objects or false for failures, throw exceptions for null arguments
- **DTOs**: Suffix with "Dto", use DataContract/DataMember attributes
- **Services**: Implement interfaces, use ServiceBehavior attributes
- **Database**: Use using statements for DbContext, check for null before operations
