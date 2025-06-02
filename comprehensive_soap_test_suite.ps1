# Comprehensive SOAP Test Suite for WCF Services
# Perfect for regression testing after upgrades

param(
    [string]$BaseUrl = "http://localhost",
    [int]$CustomerPort = 8732,
    [int]$ProductPort = 8731,
    [int]$OrderPort = 8733
)

$ErrorActionPreference = "Continue"
$testResults = @()

function Test-SoapEndpoint {
    param(
        [string]$ServiceUrl,
        [string]$SoapAction,
        [string]$SoapBody,
        [string]$TestName,
        [string]$ExpectedPattern = ""
    )
    
    Write-Host "`n=== Testing: $TestName ===" -ForegroundColor Cyan
    
    try {
        $response = Invoke-WebRequest -Uri $ServiceUrl -Method Post -Body $SoapBody -ContentType "text/xml; charset=utf-8" -Headers @{"SOAPAction" = $SoapAction} -TimeoutSec 30
        
        $success = $true
        if ($ExpectedPattern -and $response.Content -notmatch $ExpectedPattern) {
            $success = $false
            Write-Host "FAIL - Expected pattern not found: $ExpectedPattern" -ForegroundColor Red
        }
        
        if ($success) {
            Write-Host "PASS - Status: $($response.StatusCode)" -ForegroundColor Green
            $script:testResults += [PSCustomObject]@{
                Test = $TestName
                Status = "PASS"
                StatusCode = $response.StatusCode
                ResponseLength = $response.Content.Length
            }
        }
        
        return $response.Content
        
    } catch {
        Write-Host "FAIL - Error: $($_.Exception.Message)" -ForegroundColor Red
        $script:testResults += [PSCustomObject]@{
            Test = $TestName
            Status = "FAIL"
            Error = $_.Exception.Message
        }
        return $null
    }
}

# Test Data
$testCustomer = @{
    FirstName = "TestUser"
    LastName = "Regression"
    Email = "test.regression@example.com"
    Address = "123 Test Street"
    City = "Test City"
    State = "Test State"
    Country = "Test Country"
    Phone = "555-TEST"
    ZipCode = "12345"
}

Write-Host "Starting Comprehensive SOAP Service Test Suite" -ForegroundColor Yellow
Write-Host "Testing against: Customer($CustomerPort), Product($ProductPort), Order($OrderPort)"

# ============================================================================
# CUSTOMER SERVICE TESTS
# ============================================================================

$customerServiceUrl = "$BaseUrl`:$CustomerPort/CustomerService/"

# Test 1: Get All Customers
$getAllCustomersBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
  <soap:Header/>
  <soap:Body>
    <tem:GetAllCustomers/>
  </soap:Body>
</soap:Envelope>
"@

Test-SoapEndpoint -ServiceUrl $customerServiceUrl -SoapAction "http://tempuri.org/ICustomerService/GetAllCustomers" -SoapBody $getAllCustomersBody -TestName "Customer: Get All Customers" -ExpectedPattern "GetAllCustomersResponse"

# Test 2: Create Customer
$createCustomerBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
               xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <CreateCustomer xmlns="http://tempuri.org/">
      <customer xmlns:a="http://schemas.datacontract.org/2004/07/SampleEcomStoreApi.Contracts.DataContracts" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
        <a:Address>$($testCustomer.Address)</a:Address>
        <a:City>$($testCustomer.City)</a:City>
        <a:Country>$($testCustomer.Country)</a:Country>
        <a:CreatedDate>2025-06-01T00:00:00</a:CreatedDate>
        <a:CustomerId>0</a:CustomerId>
        <a:Email>$($testCustomer.Email)</a:Email>
        <a:FirstName>$($testCustomer.FirstName)</a:FirstName>
        <a:IsActive>true</a:IsActive>
        <a:LastName>$($testCustomer.LastName)</a:LastName>
        <a:ModifiedDate>2025-06-01T00:00:00</a:ModifiedDate>
        <a:Phone>$($testCustomer.Phone)</a:Phone>
        <a:State>$($testCustomer.State)</a:State>
        <a:ZipCode>$($testCustomer.ZipCode)</a:ZipCode>
      </customer>
    </CreateCustomer>
  </soap:Body>
</soap:Envelope>
"@

$createResponse = Test-SoapEndpoint -ServiceUrl $customerServiceUrl -SoapAction "http://tempuri.org/ICustomerService/CreateCustomer" -SoapBody $createCustomerBody -TestName "Customer: Create New Customer" -ExpectedPattern "CreateCustomerResult"

# Extract customer ID from response
$newCustomerId = $null
if ($createResponse -and $createResponse -match '<CreateCustomerResult>(\d+)</CreateCustomerResult>') {
    $newCustomerId = $matches[1]
    Write-Host "Created customer with ID: $newCustomerId" -ForegroundColor Green
}

# Test 3: Get Customer by ID (if we created one)
if ($newCustomerId) {
    $getCustomerByIdBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
  <soap:Header/>
  <soap:Body>
    <tem:GetCustomerById>
      <tem:customerId>$newCustomerId</tem:customerId>
    </tem:GetCustomerById>
  </soap:Body>
</soap:Envelope>
"@

    Test-SoapEndpoint -ServiceUrl $customerServiceUrl -SoapAction "http://tempuri.org/ICustomerService/GetCustomerById" -SoapBody $getCustomerByIdBody -TestName "Customer: Get Customer By ID" -ExpectedPattern "GetCustomerByIdResponse"
}

# Test 4: Get Customer by Email
$getCustomerByEmailBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
  <soap:Header/>
  <soap:Body>
    <tem:GetCustomerByEmail>
      <tem:email>$($testCustomer.Email)</tem:email>
    </tem:GetCustomerByEmail>
  </soap:Body>
</soap:Envelope>
"@

Test-SoapEndpoint -ServiceUrl $customerServiceUrl -SoapAction "http://tempuri.org/ICustomerService/GetCustomerByEmail" -SoapBody $getCustomerByEmailBody -TestName "Customer: Get Customer By Email" -ExpectedPattern "GetCustomerByEmailResponse"

# ============================================================================
# PRODUCT SERVICE TESTS
# ============================================================================

$productServiceUrl = "$BaseUrl`:$ProductPort/ProductService/"

# Test 5: Get All Products
$getAllProductsBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
  <soap:Header/>
  <soap:Body>
    <tem:GetAllProducts/>
  </soap:Body>
</soap:Envelope>
"@

Test-SoapEndpoint -ServiceUrl $productServiceUrl -SoapAction "http://tempuri.org/IProductService/GetAllProducts" -SoapBody $getAllProductsBody -TestName "Product: Get All Products" -ExpectedPattern "GetAllProductsResponse"

# Test 6: Create Product
$createProductBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
               xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <CreateProduct xmlns="http://tempuri.org/">
      <product xmlns:a="http://schemas.datacontract.org/2004/07/SampleEcomStoreApi.Contracts.DataContracts" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
        <a:Category>Test Category</a:Category>
        <a:CreatedDate>2025-06-01T00:00:00</a:CreatedDate>
        <a:Description>Test Product for Regression Testing</a:Description>
        <a:IsActive>true</a:IsActive>
        <a:ModifiedDate>2025-06-01T00:00:00</a:ModifiedDate>
        <a:Name>Test Product</a:Name>
        <a:Price>99.99</a:Price>
        <a:ProductId>0</a:ProductId>
        <a:StockQuantity>100</a:StockQuantity>
      </product>
    </CreateProduct>
  </soap:Body>
</soap:Envelope>
"@

Test-SoapEndpoint -ServiceUrl $productServiceUrl -SoapAction "http://tempuri.org/IProductService/CreateProduct" -SoapBody $createProductBody -TestName "Product: Create New Product" -ExpectedPattern "CreateProductResult"

# ============================================================================
# ORDER SERVICE TESTS
# ============================================================================

$orderServiceUrl = "$BaseUrl`:$OrderPort/OrderService/"

# Test 7: Get All Orders
$getAllOrdersBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
  <soap:Header/>
  <soap:Body>
    <tem:GetAllOrders/>
  </soap:Body>
</soap:Envelope>
"@

Test-SoapEndpoint -ServiceUrl $orderServiceUrl -SoapAction "http://tempuri.org/IOrderService/GetAllOrders" -SoapBody $getAllOrdersBody -TestName "Order: Get All Orders" -ExpectedPattern "GetAllOrdersResponse"

# ============================================================================
# TEST SUMMARY
# ============================================================================

Write-Host "`n" + "="*60 -ForegroundColor Yellow
Write-Host "TEST SUITE SUMMARY" -ForegroundColor Yellow
Write-Host "="*60 -ForegroundColor Yellow

$passCount = ($testResults | Where-Object { $_.Status -eq "PASS" }).Count
$failCount = ($testResults | Where-Object { $_.Status -eq "FAIL" }).Count
$totalTests = $testResults.Count

Write-Host "Total Tests: $totalTests" -ForegroundColor White
Write-Host "Passed: $passCount" -ForegroundColor Green
Write-Host "Failed: $failCount" -ForegroundColor Red

if ($failCount -gt 0) {
    Write-Host "`nFailed Tests:" -ForegroundColor Red
    $testResults | Where-Object { $_.Status -eq "FAIL" } | ForEach-Object {
        Write-Host "- $($_.Test): $($_.Error)" -ForegroundColor Red
    }
}

# Export results to CSV for reporting
$testResults | Export-Csv -Path "soap_test_results_$(Get-Date -Format 'yyyyMMdd_HHmmss').csv" -NoTypeInformation
Write-Host "`nDetailed results exported to CSV file" -ForegroundColor Cyan

# Return exit code for CI/CD
if ($failCount -eq 0) {
    Write-Host "`nALL TESTS PASSED! ✓" -ForegroundColor Green
    exit 0
} else {
    Write-Host "`nSOME TESTS FAILED! ✗" -ForegroundColor Red
    exit 1
}
