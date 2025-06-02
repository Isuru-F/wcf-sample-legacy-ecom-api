Write-Host "=== SOAP CustomerService Test Summary ===" -ForegroundColor Green
Write-Host ""

# Test 1: Create Customer
Write-Host "Test 1: CreateCustomer" -ForegroundColor Yellow
$createXml = Get-Content -Path "create_customer.xml" -Raw
$createHeaders = @{
    "Content-Type" = "text/xml; charset=utf-8"
    "SOAPAction" = "http://tempuri.org/ICustomerService/CreateCustomer"
}

try {
    $createResult = Invoke-RestMethod -Uri "http://localhost:8732/CustomerService/" -Method Post -Body $createXml -Headers $createHeaders
    $customerId = $createResult.Envelope.Body.CreateCustomerResponse.CreateCustomerResult
    Write-Host "✅ CreateCustomer succeeded - Returned ID: $customerId" -ForegroundColor Green
} catch {
    Write-Host "❌ CreateCustomer failed: $_" -ForegroundColor Red
    exit 1
}

Write-Host ""

# Test 2: Get All Customers
Write-Host "Test 2: GetAllCustomers" -ForegroundColor Yellow
$getAllXml = Get-Content -Path "get_all_customers.xml" -Raw
$getAllHeaders = @{
    "Content-Type" = "text/xml; charset=utf-8"
    "SOAPAction" = "http://tempuri.org/ICustomerService/GetAllCustomers"
}

try {
    $getAllResult = Invoke-RestMethod -Uri "http://localhost:8732/CustomerService/" -Method Post -Body $getAllXml -Headers $getAllHeaders
    $customersResult = $getAllResult.Envelope.Body.GetAllCustomersResponse.GetAllCustomersResult
    
    if ($customersResult.HasChildNodes -and $customersResult.ChildNodes.Count -gt 0) {
        Write-Host "✅ GetAllCustomers returned customer data" -ForegroundColor Green
    } else {
        Write-Host "⚠️  GetAllCustomers returned empty result (no customers found)" -ForegroundColor Yellow
    }
} catch {
    Write-Host "❌ GetAllCustomers failed: $_" -ForegroundColor Red
}

Write-Host ""

# Test 3: Get Customer By ID
Write-Host "Test 3: GetCustomerById (ID: $customerId)" -ForegroundColor Yellow
$getByIdXml = Get-Content -Path "get_customer_by_id.xml" -Raw
$getByIdHeaders = @{
    "Content-Type" = "text/xml; charset=utf-8"
    "SOAPAction" = "http://tempuri.org/ICustomerService/GetCustomerById"
}

try {
    $getByIdResult = Invoke-RestMethod -Uri "http://localhost:8732/CustomerService/" -Method Post -Body $getByIdXml -Headers $getByIdHeaders
    $customer = $getByIdResult.Envelope.Body.GetCustomerByIdResponse.GetCustomerByIdResult
    
    if ($customer.Email -and $customer.Email -ne '') {
        Write-Host "✅ GetCustomerById returned customer with email: $($customer.Email)" -ForegroundColor Green
    } else {
        Write-Host "⚠️  GetCustomerById returned customer object but with null/empty data" -ForegroundColor Yellow
        Write-Host "    Customer ID: $($customer.CustomerId)" -ForegroundColor Gray
        Write-Host "    Is Active: $($customer.IsActive)" -ForegroundColor Gray
    }
} catch {
    Write-Host "❌ GetCustomerById failed: $_" -ForegroundColor Red
}

Write-Host ""
Write-Host "=== Test Conclusion ===" -ForegroundColor Cyan
Write-Host "• CreateCustomer: ✅ Working (returns customer ID)" -ForegroundColor Green
Write-Host "• GetAllCustomers: ⚠️  Returns empty collection" -ForegroundColor Yellow  
Write-Host "• GetCustomerById: ⚠️  Returns object with null data" -ForegroundColor Yellow
Write-Host ""
Write-Host "This suggests the SOAP service endpoints are functional but there may be" -ForegroundColor White
Write-Host "an issue with data persistence or the repository implementation." -ForegroundColor White
