@echo off
echo ===============================================
echo  Sample Ecommerce Store API Test Client
echo ===============================================
echo.

echo Checking if WCF services are running...
netstat -an | findstr ":8732" >nul
if errorlevel 1 (
    echo WARNING: WCF services don't appear to be running on port 8732
    echo Please start the SampleEcomStoreApi.ConsoleHost.exe first
    echo.
    pause
    goto :eof
)

echo WCF services detected. Building test client...
echo.

dotnet build
if errorlevel 1 (
    echo Build failed!
    pause
    goto :eof
)

echo.
echo Build successful! Starting test client...
echo.
dotnet run --project TestClient

pause
