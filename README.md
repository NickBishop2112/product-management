# Product Management Dashboard

## Overview
The Product Management Dashboard contains the following features:
- Product database 
- Product API (`http://localhost:5002/swagger/index.html`)
- Dashboard (`http://localhost:3000/`)

## Prerequistes

- Database tools are installed:
    - SQL Server Express
    - sqlcmd
    - SQL Server Management Studio (SSMS) if needed
- .Net SDK 8 is installed
- Rider or Visual Studio is installed if needed
- NodeJs is installed

## Step 1 - clone the Git repo
```
git clone <?>
```

## Step 2 - Run the app
The instruction are:
1. Open a command prompt such as a vscode terminal. 
2. Go to the `product-management` folder
3. Run the app:
```
run-app.bat
```

## Step 3 - Testing
### Register a product
Navigate to `http://localhost:8080/swagger/index.html` and select the Register endpoint.

### View the dashboard
Start the browser and go to `http://localhost:3000/`. Press f5 from the brower to refresh.

## Step 4 - Stopping the app
Kill the dotnet api processes:
```
taskkill /F /IM dotnet.exe
```