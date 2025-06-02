# Complete CreateCustomer SOAP request
$soapBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <CreateCustomer xmlns="http://tempuri.org/">
      <customer xmlns:a="http://schemas.datacontract.org/2004/07/SampleEcomStoreApi.Contracts.DataContracts" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
        <a:Address>129 West 81st Street, Apt 5B</a:Address>
        <a:City>New York</a:City>
        <a:Country>USA</a:Country>
        <a:CreatedDate>2025-06-01T00:00:00</a:CreatedDate>
        <a:CustomerId>0</a:CustomerId>
        <a:Email>cosmo.kramer@email.com</a:Email>
        <a:FirstName>Cosmo</a:FirstName>
        <a:IsActive>true</a:IsActive>
        <a:LastName>Kramer</a:LastName>
        <a:ModifiedDate>2025-06-01T00:00:00</a:ModifiedDate>
        <a:Phone>555-KRAMER</a:Phone>
        <a:State>NY</a:State>
        <a:ZipCode>10024</a:ZipCode>
      </customer>
    </CreateCustomer>
  </soap:Body>
</soap:Envelope>
"@

$uri = "http://localhost:8732/CustomerService/"
$action = "http://tempuri.org/ICustomerService/CreateCustomer"

try {
    $response = Invoke-WebRequest -Uri $uri -Method Post -Body $soapBody -ContentType "text/xml; charset=utf-8" -Headers @{"SOAPAction" = $action}
    Write-Host "SUCCESS!"
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
