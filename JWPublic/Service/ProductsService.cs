using JWPublic.DTO;
using System.Net.Http.Json;

namespace JWPublic.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductsDTO>> GetTop20ProductsAsync()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<ProductsDTO>>("/api/Products/GetAllProducts");

                if (products == null || !products.Any())
                    return new List<ProductsDTO>();

                return products
                    .OrderByDescending(p => p.Productid)
                    .Take(20)
                    .ToList();
            }
            catch (Exception ex)
            {
                // Log error here if needed
                Console.WriteLine($"Error fetching products: {ex.Message}");
                return new List<ProductsDTO>();
            }
        }

        public async Task<List<ProductsDTO>> GetAllProductsAsync()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<ProductsDTO>>("/api/Products/GetAllProducts");
                return products ?? new List<ProductsDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching products: {ex.Message}");
                return new List<ProductsDTO>();
            }
        }
    }
}