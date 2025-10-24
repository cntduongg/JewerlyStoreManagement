using ProductsApi.Models;

namespace ProductsApi.Repo.IRepo
{
    public interface IProductsRepo
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Category>> GetAllCategoriesAsync();
    }
}
