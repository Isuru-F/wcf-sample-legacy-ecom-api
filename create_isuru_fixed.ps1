# Create customer isuru fonseka with correct SOAP format
$soapBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
               xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <CreateCustomer xmlns="http://tempuri.org/">
      <customerDto xmlns:a="http://schemas.datacontract.org/2004/07/SampleEcomStoreApi.Contracts.DataContracts" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
        <a:Address>123 Main Street</a:Address>
        <a:City>Colombo</a:City>
        <a:Country>Sri Lanka</a:Country>
        <a:CreatedDate>2025-06-01T00:00:00</a:CreatedDate>
        <a:CustomerId>0</a:CustomerId>
        <a:Email>isuru.fonseka@email.com</a:Email>
        <a:FirstName>isuru</a:FirstName>
        <a:IsActive>true</a:IsActive>
        <a:LastName>fonseka</a:LastName>
        <a:ModifiedDate>2025-06-01T00:00:00</a:ModifiedDate>
        <a:Phone>555-1234</a:Phone>
        <a:State>Western</a:State>
        <a:ZipCode>10100</a:ZipCode>
      </customerDto>
    </CreateCustomer>
  </soap:Body>
</soap:Envelope>
"@

$uri = "http://localhost:8732/CustomerService/"
$action = "http://tempuri.org/ICustomerService/CreateCustomer"

Write-Host "Creating customer: isuru fonseka (with correct format)"

try {
    $response = Invoke-WebRequest -Uri $uri -Method Post -Body $soapBody -ContentType "text/xml; charset=utf-8" -Headers @{"SOAPAction" = $action}
    Write-Host "SUCCESS - Response Status: $($response.StatusCode)"
    Write-Host "Response Content:"
    Write-Host $response.Content
    
    # Extract customer ID from response
    if ($response.Content -match '<CreateCustomerResult>(\d+)</CreateCustomerResult>') {
        $customerId = $matches[1]
        Write-Host ""
        Write-Host "Customer created successfully with ID: $customerId"
    }
    
} catch {
    Write-Host "Error: $($_.Exception.Message)"
    if ($_.Exception.Response) {
        try {
            $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
            $responseText = $reader.ReadToEnd()
            Write-Host "Response Body:"
            Write-Host $responseText
        } catch {
            Write-Host "Could not read error response"
        }
    }
}
