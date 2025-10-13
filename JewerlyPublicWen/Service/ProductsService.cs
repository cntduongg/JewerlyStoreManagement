using JewerlyPublicWen.Models.Dtos;
using JewerlyPublicWen.Service.IService;
using System.Text.Json;

namespace JewerlyPublicWen.Service
{
    public class ProductsService : IProductsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ProductsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public  async  Task<List<ProductsDto>> GetAllProductsAsync()
        {
            try
            {
                // URL API từ BE - sử dụng localhost:7114 như trong JavaScript
                var apiUrl = "https://localhost:7114/api/Products/GetAllProducts";

                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var products = JsonSerializer.Deserialize<List<ProductsDto>>(content, options);
                    return products ?? new List<ProductsDto>();
                }

                // Log lỗi nếu cần
                Console.WriteLine($"API Error: {response.StatusCode}");
                return new List<ProductsDto>();
            }
            catch (Exception ex)
            {
                // Log lỗi
                Console.WriteLine($"Service Error: {ex.Message}");
                return new List<ProductsDto>();
            }
        }
    }
    }

