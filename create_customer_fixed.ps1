# Create customer with correct parameter name
$soapBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
               xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <CreateCustomer xmlns="http://tempuri.org/">
      <customer xmlns:a="http://schemas.datacontract.org/2004/07/SampleEcomStoreApi.Contracts.DataContracts" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
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
      </customer>
    </CreateCustomer>
  </soap:Body>
</soap:Envelope>
"@

$uri = "http://localhost:8732/CustomerService/"
$action = "http://tempuri.org/ICustomerService/CreateCustomer"

Write-Host "Creating customer: isuru fonseka (with correct parameter name)"

try {
    $response = Invoke-WebRequest -Uri $uri -Method Post -Body $soapBody -ContentType "text/xml; charset=utf-8" -Headers @{"SOAPAction" = $action}
    Write-Host "SUCCESS - Response Status: $($response.StatusCode)"
    Write-Host "Response Content:"
    Write-Host $response.Content
    
    if ($response.Content -match '<CreateCustomerResult>(\d+)</CreateCustomerResult>') {
        $customerId = $matches[1]
        Write-Host ""
        Write-Host "SUCCESS: Customer created with ID: $customerId"
    }
    
} catch [System.Net.WebException] {
    Write-Host "WebException: $($_.Exception.Message)"
    if ($_.Exception.Response) {
        try {
            $errorStream = $_.Exception.Response.GetResponseStream()
            $errorReader = New-Object System.IO.StreamReader($errorStream)
            $errorText = $errorReader.ReadToEnd()
            Write-Host "Error Response:"
            Write-Host $errorText
        } catch {
            Write-Host "Could not read error response"
        }
    }
} catch {
    Write-Host "Error: $($_.Exception.Message)"
}
