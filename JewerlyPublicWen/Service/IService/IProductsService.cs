using JewerlyPublicWen.Models.Dtos;

namespace JewerlyPublicWen.Service.IService
{
    public interface IProductsService
    {
        Task<List<ProductsDto>> GetAllProductsAsync();
    }
}
