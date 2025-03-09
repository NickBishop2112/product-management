using Dapper;
using Microsoft.Data.SqlClient;
using ProductManagement.API.Model;
using Task = System.Threading.Tasks.Task;

namespace ProductManagement.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;
    
    public ProductRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task Register(Product product)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
         var sql = @"
             DECLARE @categoryId INT;
             DECLARE @trimmedCategory VARCHAR(50) = TRIM(@Category);
   
             INSERT INTO Category (Name)
             SELECT @trimmedCategory
             WHERE NOT EXISTS (SELECT 1 FROM Category WHERE Name = @trimmedCategory);
         
             SELECT @categoryId = CategoryId 
             FROM Category 
             WHERE Name = @trimmedCategory; 
             
             DECLARE @trimmedProductCode VARCHAR(50) = TRIM(@productCode);

             INSERT INTO Product (ProductCode, CategoryId, Name, Price, StockQuantity, DateAdded)
             SELECT 
                   @trimmedProductCode
                 , @categoryId
                 , TRIM(@Name) 
                 , @Price 
                 , @StockQuantity
                 , @DateAdded
             WHERE NOT EXISTS (SELECT 1 FROM Product WHERE ProductCode = @trimmedProductCode)
         ";
        
        await connection.ExecuteAsync(sql, product);
    }

    public async Task<System.Collections.Generic.IEnumerable<Product>> GetDetails()
    {
        await using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var sql = @$"
            SELECT 
                p.ProductCode,
                c.Name AS Category,
                p.Name,
                p.Price,
                p.StockQuantity,
                p.DateAdded
            FROM Product p
            JOIN Category c ON p.CategoryId = c.CategoryId
            ORDER BY p.ProductCode";
        return await connection.QueryAsync<Product>(sql);
    }
}

