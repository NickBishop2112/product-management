using ProductManagement.API.Model;

namespace ProductManagement.API.Repositories;

public interface IProductRepository
{
    Task Register(Product product);
    Task<IEnumerable<Product>> GetDetails();
}