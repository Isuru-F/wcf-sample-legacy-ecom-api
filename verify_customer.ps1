# Verify if isuru fonseka customer exists
$soapBody = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
  <soap:Header/>
  <soap:Body>
    <tem:GetAllCustomers/>
  </soap:Body>
</soap:Envelope>
"@

$uri = "http://localhost:8732/CustomerService/"
$action = "http://tempuri.org/ICustomerService/GetAllCustomers"

Write-Host "Checking if customer 'isuru fonseka' exists..."

try {
    $response = Invoke-WebRequest -Uri $uri -Method Post -Body $soapBody -ContentType "text/xml; charset=utf-8" -Headers @{"SOAPAction" = $action}
    
    $content = $response.Content
    Write-Host "Response received, searching for customer..."
    
    if ($content -match "isuru" -or $content -match "fonseka") {
        Write-Host "SUCCESS: Customer 'isuru fonseka' found!"
        # Show the full response to see the customer details
        Write-Host "Full response:"
        Write-Host $content
    } else {
        Write-Host "Customer 'isuru fonseka' NOT found in database"
        Write-Host ""
        Write-Host "Current customers found:"
        
        # Extract customer names from the response
        $matches = [regex]::Matches($content, '<a:FirstName>(.*?)</a:FirstName>.*?<a:LastName>(.*?)</a:LastName>')
        $customerCount = 0
        foreach ($match in $matches) {
            $customerCount++
            $firstName = $match.Groups[1].Value
            $lastName = $match.Groups[2].Value
            Write-Host "$customerCount. $firstName $lastName"
        }
        
        if ($customerCount -eq 0) {
            Write-Host "No customers found in response"
        }
    }
    
} catch {
    Write-Host "Error: $($_.Exception.Message)"
}
