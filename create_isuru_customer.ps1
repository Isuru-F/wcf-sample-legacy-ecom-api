# Create customer isuru fonseka with detailed error handling
$soapBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <CreateCustomer xmlns="http://tempuri.org/">
      <customer xmlns:a="http://schemas.datacontract.org/2004/07/SampleEcomStoreApi.Contracts.DataContracts">
        <a:FirstName>isuru</a:FirstName>
        <a:LastName>fonseka</a:LastName>
        <a:Email>isuru.fonseka@email.com</a:Email>
        <a:IsActive>true</a:IsActive>
      </customer>
    </CreateCustomer>
  </soap:Body>
</soap:Envelope>
"@

$uri = "http://localhost:8732/CustomerService/"
$action = "http://tempuri.org/ICustomerService/CreateCustomer"

Write-Host "Creating customer: isuru fonseka"
Write-Host "URI: $uri"
Write-Host "Action: $action"
Write-Host ""

try {
    $response = Invoke-WebRequest -Uri $uri -Method Post -Body $soapBody -ContentType "text/xml; charset=utf-8" -Headers @{"SOAPAction" = $action} -Verbose
    
    Write-Host "SUCCESS - Response Status: $($response.StatusCode)"
    Write-Host "Response Headers:"
    $response.Headers | Format-Table
    Write-Host ""
    Write-Host "Response Content:"
    Write-Host $response.Content
    Write-Host ""
    
    # Check if response contains a customer ID
    if ($response.Content -match '<CreateCustomerResult>(\d+)</CreateCustomerResult>') {
        $customerId = $matches[1]
        Write-Host "SUCCESS: Customer created with ID: $customerId"
    } else {
        Write-Host "WARNING: Could not extract customer ID from response"
    }
    
} catch {
    Write-Host "ERROR occurred during customer creation:"
    Write-Host "Exception: $($_.Exception.GetType().Name)"
    Write-Host "Message: $($_.Exception.Message)"
    
    if ($_.Exception.Response) {
        Write-Host ""
        Write-Host "HTTP Response Details:"
        Write-Host "Status Code: $($_.Exception.Response.StatusCode)"
        Write-Host "Status Description: $($_.Exception.Response.StatusDescription)"
        
        try {
            $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
            $responseText = $reader.ReadToEnd()
            Write-Host "Response Body:"
            Write-Host $responseText
        } catch {
            Write-Host "Could not read error response body"
        }
    }
    
    Write-Host ""
    Write-Host "Full Exception Details:"
    Write-Host $_.Exception | Format-List * -Force
}
