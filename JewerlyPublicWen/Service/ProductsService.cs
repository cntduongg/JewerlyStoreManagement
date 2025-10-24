using JewerlyPublicWen.Models;
using JewerlyPublicWen.Models.Dtos;
using JewerlyPublicWen.Service.IService;
using System.Net.Http;
using System.Text.Json;

namespace JewerlyPublicWen.Service
{
    public class ProductsService : IProductsService
    {
        private readonly HttpClient _httpClient;

        public ProductsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7114/api/Products/GetAllCategories");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<CategoryDto>>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return categories ?? new List<CategoryDto>();
        }

        public async Task<List<ProductsViewModel>> GetAllProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Products/GetAllProducts");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<ProductsViewModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return products ?? new List<ProductsViewModel>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi gọi API: {ex.Message}", ex);
            }
        }

        
    }
}