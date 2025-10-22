using ProductsApi.Models;

namespace ProductsApi.Repo.IRepo
{
    public interface IProductsRepo
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> GetByProductCodeAsync(string productCode);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ProductCodeExistsAsync(string productCode, int? excludeProductId = null);
    }
}