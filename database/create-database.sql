IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'Product')
BEGIN
    CREATE DATABASE Product
END

SELECT name FROM sys.databases
GO

USE Product
GO

ALTER AUTHORIZATION ON DATABASE::Product TO sa;
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Category')
BEGIN
    CREATE TABLE Category
    (
          CategoryId INT  IDENTITY(1,1) PRIMARY KEY
        , Name VARCHAR(100) NOT NULL
    )
END

GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Product')
BEGIN
    CREATE TABLE Product
    (
          ProductCode VARCHAR(30) NOT NULL PRIMARY KEY 
        , CategoryId INT NOT NULL      
        , Name VARCHAR(100) NOT NULL        
        , Price DECIMAL(10,2) NOT NULL CHECK (Price > 0)
        , StockQuantity INT NOT NULL CHECK (StockQuantity >= 0)
        , DateAdded DATETIME NOT NULL
        , CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId) 
    )
END

GO

SELECT name FROM sys.tables
GO