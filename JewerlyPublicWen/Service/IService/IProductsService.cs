using JewerlyPublicWen.Models;
using JewerlyPublicWen.Models.Dtos;

namespace JewerlyPublicWen.Service.IService
{
    public interface IProductsService
    {
        Task<List<ProductsViewModel>> GetAllProductsAsync();
        Task<List<CategoryDto>> GetAllCategoriesAsync();
    }
}
