# Debug CreateCustomer with detailed error output
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

Write-Host "Creating customer: isuru fonseka"
Write-Host "URI: $uri"
Write-Host "Action: $action"
Write-Host ""

try {
    # Enable detailed error information
    $ErrorActionPreference = "Stop"
    
    $request = [System.Net.WebRequest]::Create($uri)
    $request.Method = "POST"
    $request.ContentType = "text/xml; charset=utf-8"
    $request.Headers.Add("SOAPAction", $action)
    
    $bytes = [System.Text.Encoding]::UTF8.GetBytes($soapBody)
    $request.ContentLength = $bytes.Length
    
    $requestStream = $request.GetRequestStream()
    $requestStream.Write($bytes, 0, $bytes.Length)
    $requestStream.Close()
    
    Write-Host "Request sent, waiting for response..."
    
    $response = $request.GetResponse()
    $responseStream = $response.GetResponseStream()
    $reader = New-Object System.IO.StreamReader($responseStream)
    $responseText = $reader.ReadToEnd()
    
    Write-Host "SUCCESS - Response Status: $($response.StatusCode)"
    Write-Host "Response Content:"
    Write-Host $responseText
    
    if ($responseText -match '<CreateCustomerResult>(\d+)</CreateCustomerResult>') {
        $customerId = $matches[1]
        Write-Host ""
        Write-Host "Customer created with ID: $customerId"
    }
    
} catch [System.Net.WebException] {
    Write-Host "WebException occurred:"
    Write-Host "Status: $($_.Exception.Status)"
    Write-Host "Message: $($_.Exception.Message)"
    
    if ($_.Exception.Response) {
        Write-Host "HTTP Status: $($_.Exception.Response.StatusCode)"
        Write-Host "Status Description: $($_.Exception.Response.StatusDescription)"
        
        try {
            $errorStream = $_.Exception.Response.GetResponseStream()
            $errorReader = New-Object System.IO.StreamReader($errorStream)
            $errorText = $errorReader.ReadToEnd()
            Write-Host ""
            Write-Host "Error Response Body:"
            Write-Host $errorText
        } catch {
            Write-Host "Could not read error response"
        }
    }
} catch {
    Write-Host "General Exception:"
    Write-Host "Type: $($_.Exception.GetType().Name)"
    Write-Host "Message: $($_.Exception.Message)"
    Write-Host "Stack Trace:"
    Write-Host $_.Exception.StackTrace
}
