<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sample Ecommerce Store API</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 40px; }
        .service-link { display: block; margin: 10px 0; padding: 10px; background-color: #f0f0f0; text-decoration: none; color: #333; }
        .service-link:hover { background-color: #e0e0e0; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Sample Ecommerce Store API</h1>
            <p>Welcome to the WCF Services for the Sample Ecommerce Store.</p>
            
            <h2>Available Services:</h2>
            <a href="ProductService.svc" class="service-link">Product Service (ProductService.svc)</a>
            <a href="CustomerService.svc" class="service-link">Customer Service (CustomerService.svc)</a>
            <a href="OrderService.svc" class="service-link">Order Service (OrderService.svc)</a>
            
            <h2>WSDL URLs:</h2>
            <a href="ProductService.svc?wsdl" class="service-link">Product Service WSDL</a>
            <a href="CustomerService.svc?wsdl" class="service-link">Customer Service WSDL</a>
            <a href="OrderService.svc?wsdl" class="service-link">Order Service WSDL</a>
            
            <p><strong>Status:</strong> Services are running successfully!</p>
        </div>
    </form>
</body>
</html>
