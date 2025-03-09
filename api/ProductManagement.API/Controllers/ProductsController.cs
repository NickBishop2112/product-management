using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Model;
using ProductManagement.API.Repositories;

namespace ProductManagement.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository productRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] Product product)
    {
        try
        {
            await productRepository.Register(product);
            return Ok("Success");
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"An unexpected error has occurred: ${e.Message}" });
        }
    }
    
    [HttpGet("Details")]
    public async Task<IActionResult> GetDetails()
    {
        try
        {
            var products = await productRepository.GetDetails();
            return Ok(products);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"An unexpected error has occurred: ${e.Message}" });
        }
    }
}