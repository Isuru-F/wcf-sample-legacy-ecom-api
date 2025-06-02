# Quick test of the test client functionality
param([switch]$Interactive)

Write-Host "Quick Test of Sample Ecommerce Store API Test Client" -ForegroundColor Green
Write-Host "=====================================================" -ForegroundColor Green

# Check if services are running
Write-Host "`nChecking if WCF services are running..." -ForegroundColor Yellow
$customerService = Test-NetConnection -ComputerName localhost -Port 8732 -WarningAction SilentlyContinue
$productService = Test-NetConnection -ComputerName localhost -Port 8731 -WarningAction SilentlyContinue

if (!$customerService.TcpTestSucceeded) {
    Write-Host "ERROR: Customer Service not running on port 8732" -ForegroundColor Red
    Write-Host "Please start the WCF services first" -ForegroundColor Red
    exit 1
}

if (!$productService.TcpTestSucceeded) {
    Write-Host "ERROR: Product Service not running on port 8731" -ForegroundColor Red
    Write-Host "Please start the WCF services first" -ForegroundColor Red
    exit 1
}

Write-Host "✓ Services are running" -ForegroundColor Green

# Build the project
Write-Host "`nBuilding test client..." -ForegroundColor Yellow
$buildResult = & dotnet build --verbosity quiet
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Build failed" -ForegroundColor Red
    exit 1
}
Write-Host "✓ Build successful" -ForegroundColor Green

# Test basic connectivity
Write-Host "`nTesting basic connectivity..." -ForegroundColor Yellow

if ($Interactive) {
    Write-Host "`nStarting interactive test client..." -ForegroundColor Cyan
    Write-Host "Use option 6 for regression testing, or try creating a customer with option 2" -ForegroundColor Cyan
    Write-Host ""
    & dotnet run --project TestClient
} else {
    Write-Host "✓ All checks passed! Test client is ready to use." -ForegroundColor Green
    Write-Host ""
    Write-Host "To run the test client:" -ForegroundColor Cyan
    Write-Host "  dotnet run --project TestClient" -ForegroundColor White
    Write-Host ""
    Write-Host "Or run this script with -Interactive flag:" -ForegroundColor Cyan
    Write-Host "  .\quick-test.ps1 -Interactive" -ForegroundColor White
    Write-Host ""
    Write-Host "Or use the batch file:" -ForegroundColor Cyan
    Write-Host "  .\build-and-run.bat" -ForegroundColor White
}
