@echo off

echo Build the database
sqlcmd -S localhost\SQLEXPRESS -E -i .\database\create-database.sql

echo Build and test the API
dotnet build .\api
dotnet test .\api\ProductManagement.API.Tests\ProductManagement.API.Tests.csproj

echo Starting API in the background...
start /B dotnet run --project .\api\ProductManagement.API\ProductManagement.API.csproj > output.log 2>&1
start http://localhost:5002/swagger/index.html

echo Build the databoard
cd .\dashboard
npm install --no-audit && npm start
cd ..

echo Both backend and frontend are running.
pause
