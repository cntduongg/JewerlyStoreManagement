using ProductsApi.Models.Dtos;

namespace ProductsApi.Service.IService
{
    public interface IProductsService
    {
        Task<List<ProductsDto>> GetAllAsync();
    }
}
