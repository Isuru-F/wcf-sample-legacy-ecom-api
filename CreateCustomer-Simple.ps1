# Simple CreateCustomer test
$soapBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <CreateCustomer xmlns="http://tempuri.org/">
      <customer xmlns:a="http://schemas.datacontract.org/2004/07/SampleEcomStoreApi.Contracts.DataContracts">
        <a:FirstName>Cosmo</a:FirstName>
        <a:LastName>Kramer</a:LastName>
        <a:Email>cosmo.kramer@email.com</a:Email>
        <a:IsActive>true</a:IsActive>
      </customer>
    </CreateCustomer>
  </soap:Body>
</soap:Envelope>
"@

$uri = "http://localhost:8732/CustomerService/"
$action = "http://tempuri.org/ICustomerService/CreateCustomer"

try {
    $response = Invoke-WebRequest -Uri $uri -Method Post -Body $soapBody -ContentType "text/xml; charset=utf-8" -Headers @{"SOAPAction" = $action}
    Write-Host "Response Status: $($response.StatusCode)"
    Write-Host "Response Content:"
    Write-Host $response.Content
} catch {
    Write-Host "Error: $($_.Exception.Message)"
    if ($_.Exception.Response) {
        $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
        $responseText = $reader.ReadToEnd()
        Write-Host "Response Body:"
        Write-Host $responseText
    }
}
