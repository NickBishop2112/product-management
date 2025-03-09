using System.ComponentModel.DataAnnotations;
namespace ProductManagement.API.Model;

public record Product(
    [Required(ErrorMessage = "Product Code is required.")]
    string ProductCode,
    [Required(ErrorMessage = "Category is required.")]
    string Category,
    [Required(ErrorMessage = "Product Name is required.")]
    string Name,
    [Range(0.01, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    decimal Price,
    [Range(0, int.MaxValue, ErrorMessage = "StockQuantity must be 0 or greater.")]
    int StockQuantity,
    DateTime DateAdded
);