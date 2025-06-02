#!/bin/bash

echo "=== Testing CustomerService SOAP Operations ==="
echo

# Create Customer SOAP Request
CREATE_CUSTOMER_XML='<?xml version="1.0" encoding="utf-8"?>
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
</soap:Envelope>'

echo "1. Creating a new customer..."
CREATE_RESPONSE=$(curl -s -X POST \
  -H "Content-Type: text/xml; charset=utf-8" \
  -H "SOAPAction: http://tempuri.org/ICustomerService/CreateCustomer" \
  -d "$CREATE_CUSTOMER_XML" \
  http://localhost:8732/CustomerService/)

echo "Create Customer Response:"
echo "$CREATE_RESPONSE" | xmllint --format -
echo

# Get All Customers SOAP Request
GET_ALL_CUSTOMERS_XML='<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
  <soap:Header/>
  <soap:Body>
    <tem:GetAllCustomers/>
  </soap:Body>
</soap:Envelope>'

echo "2. Getting all customers to verify creation..."
GET_ALL_RESPONSE=$(curl -s -X POST \
  -H "Content-Type: text/xml; charset=utf-8" \
  -H "SOAPAction: http://tempuri.org/ICustomerService/GetAllCustomers" \
  -d "$GET_ALL_CUSTOMERS_XML" \
  http://localhost:8732/CustomerService/)

echo "Get All Customers Response:"
echo "$GET_ALL_RESPONSE" | xmllint --format -
echo

# Check if our test customer exists in the response
if echo "$GET_ALL_RESPONSE" | grep -q "john.doe@test.com"; then
    echo "✅ SUCCESS: Test customer found in the customer list!"
else
    echo "❌ FAILURE: Test customer not found in the customer list!"
fi
