Write-Host "=== Testing CustomerService SOAP Operations ===" -ForegroundColor Green
Write-Host ""

# Create Customer SOAP Request
$createCustomerXml = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/" xmlns:sam="http://schemas.datacontract.org/2004/07/SampleEcomStoreApi.Contracts.DataContracts">
  <soap:Header/>
  <soap:Body>
    <tem:CreateCustomer>
      <tem:customer>
        <sam:Address>123 Test Street</sam:Address>
        <sam:City>Test City</sam:City>
        <sam:Country>Test Country</sam:Country>
        <sam:CustomerId>0</sam:CustomerId>
        <sam:Email>john.doe@test.com</sam:Email>
        <sam:FirstName>John</sam:FirstName>
        <sam:IsActive>true</sam:IsActive>
        <sam:LastName>Doe</sam:LastName>
        <sam:Phone>555-123-4567</sam:Phone>
        <sam:State>Test State</sam:State>
        <sam:ZipCode>12345</sam:ZipCode>
      </tem:customer>
    </tem:CreateCustomer>
  </soap:Body>
</soap:Envelope>
"@

Write-Host "1. Creating a new customer..." -ForegroundColor Yellow

$headers = @{
    "Content-Type" = "text/xml; charset=utf-8"
    "SOAPAction" = "http://tempuri.org/ICustomerService/CreateCustomer"
}

try {
    $createResponse = Invoke-RestMethod -Uri "http://localhost:8732/CustomerService/" -Method Post -Body $createCustomerXml -Headers $headers
    Write-Host "Create Customer Response:" -ForegroundColor Cyan
    Write-Host $createResponse.OuterXml
    Write-Host ""
} catch {
    Write-Host "Error creating customer: $_" -ForegroundColor Red
    exit 1
}

# Get All Customers SOAP Request
$getAllCustomersXml = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
  <soap:Header/>
  <soap:Body>
    <tem:GetAllCustomers/>
  </soap:Body>
</soap:Envelope>
"@

Write-Host "2. Getting all customers to verify creation..." -ForegroundColor Yellow

$headers2 = @{
    "Content-Type" = "text/xml; charset=utf-8"
    "SOAPAction" = "http://tempuri.org/ICustomerService/GetAllCustomers"
}

try {
    $getAllResponse = Invoke-RestMethod -Uri "http://localhost:8732/CustomerService/" -Method Post -Body $getAllCustomersXml -Headers $headers2
    Write-Host "Get All Customers Response:" -ForegroundColor Cyan
    Write-Host $getAllResponse.OuterXml
    Write-Host ""
    
    # Check if our test customer exists in the response
    if ($getAllResponse.OuterXml -match "john.doe@test.com") {
        Write-Host "✅ SUCCESS: Test customer found in the customer list!" -ForegroundColor Green
    } else {
        Write-Host "❌ FAILURE: Test customer not found in the customer list!" -ForegroundColor Red
    }
} catch {
    Write-Host "Error getting customers: $_" -ForegroundColor Red
}
