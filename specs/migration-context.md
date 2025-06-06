# Migration Context & Entry Point

**🎯 ENTRY POINT FOR NEW AGENTS**

This file provides complete context for any agent picking up the .NET 4.5 to .NET Core 8 migration project.

## Current Project State

### Project Overview
- **Legacy System**: .NET Framework 4.5 WCF services
- **Target System**: .NET Core 8 REST API with WCF compatibility
- **Critical Requirement**: Maintain 100% backward compatibility for existing test client

### Test Client Compatibility Requirement
- **Test Client Location**: `legacy-api-test-client/` folder
- **Must Continue Working**: Throughout entire migration without changes
- **Validation Method**: Run test client after each phase to verify compatibility

### Current Architecture
```
src/
├── SampleEcomStoreApi.Contracts/      # Service contracts (WCF)
├── SampleEcomStoreApi.Common/         # Shared utilities  
├── SampleEcomStoreApi.DataAccess/     # EF 6.5.1 repositories
├── SampleEcomStoreApi.BusinessLogic/  # Business rules
├── SampleEcomStoreApi.Services/       # WCF service implementations
├── SampleEcomStoreApi.Host/           # IIS hosting (.svc files)
└── Other projects...
```

## Migration Progress Tracking

**📋 ALWAYS CHECK**: [progress.md](progress.md) - Master task tracking file

## Phase Structure

| Phase | File | Status | Description |
|-------|------|--------|-------------|
| 0 | [phase-0-baseline.md](phase-0-baseline.md) | ⏳ Pending | Test coverage baseline |
| 1 | [phase-1-foundation.md](phase-1-foundation.md) | ⏳ Pending | .NET Core 8 infrastructure |
| 2 | [phase-2-api-layer.md](phase-2-api-layer.md) | ⏳ Pending | REST API + WCF compatibility |
| 3 | [phase-3-client-migration.md](phase-3-client-migration.md) | ⏳ Pending | Client migration & monitoring |
| 4 | [phase-4-cleanup.md](phase-4-cleanup.md) | ⏳ Pending | Legacy cleanup & optimization |

## Key Constraints

### Non-Negotiable Requirements
1. **Zero Breaking Changes**: Test client must work unchanged
2. **Compile & Test**: Every step must compile and pass tests
3. **Incremental**: Small, verifiable steps with commits
4. **Rollback Ready**: Each step must be reversible

### Service Interface Compatibility
Current WCF contracts that MUST be preserved:
- `ICustomerService`: 7 operations (GetAllCustomers, GetCustomerById, etc.)
- `IProductService`: Product CRUD operations
- `IOrderService`: Order management operations

## Project Dependencies

### Current Technology Stack
- .NET Framework 4.5
- WCF Services
- Entity Framework 6.5.1
- Castle Windsor IoC
- SQLite/SQL Server database

### Target Technology Stack
- .NET Core 8
- ASP.NET Core Web API
- Entity Framework Core 8
- Microsoft.Extensions.DependencyInjection
- Same database (schema preserved)

## Getting Started Commands

### Build Current System
```bash
dotnet build SampleEcomStoreApi.sln
```

### Run Test Client
```bash
cd legacy-api-test-client
dotnet run --project TestClient
```

### Run Tests
```bash
dotnet test tests/SampleEcomStoreApi.Tests/SampleEcomStoreApi.Tests.csproj
```

## Agent Handoff Protocol

### When Starting Work
1. Read this file for complete context
2. Check [progress.md](progress.md) for current task status
3. Read the current phase file for detailed steps
4. Verify you can build and run the test client
5. Update progress.md when starting a task

### When Completing Work
1. Update progress.md with completed tasks
2. Commit changes with specified commit message
3. Verify test client still works
4. Document any issues or deviations

### When Handing Off
1. Update progress.md with current status
2. Document any blockers or decisions made
3. Note next recommended actions

## Emergency Procedures

### If Something Breaks
1. **STOP** - Do not continue
2. Check if test client still works
3. If broken, revert last commit
4. Document the issue in progress.md
5. Seek guidance before continuing

### Rollback Process
Each phase includes specific rollback steps. If needed:
1. Revert to last known good commit
2. Update progress.md to reflect rollback
3. Document lessons learned

## Quick Reference

### Current Service Endpoints (WCF)
- Customer Service: `http://localhost:8732/CustomerService/`
- Product Service: `http://localhost:8731/ProductService/`
- Order Service: `http://localhost:8733/OrderService/`

### Database Connection
- Provider: SQLite/SQL Server
- Schema: Defined in EF 6.5.1 models
- **Critical**: Schema must remain identical during migration

---

**🚀 NEXT ACTIONS**: Check [progress.md](progress.md) and start with the current pending task.
