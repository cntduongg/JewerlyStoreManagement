using ProductsApi.Models.Dtos;

namespace ProductsApi.Service.IService
{
    public interface IProductsService
    {
        Task<List<ProductsDto>> GetAllAsync();
        Task<ProductsDto> GetByIdAsync(int id);
        Task<ProductsDto> GetByProductCodeAsync(string productCode);
        Task<ProductsDto> CreateAsync(CreateProductDto createDto);
        Task<ProductsDto> UpdateAsync(UpdateProductDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}