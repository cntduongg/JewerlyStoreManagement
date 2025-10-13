using Microsoft.AspNetCore.Mvc;
using ProductsApi.Service.IService;

namespace ProductsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;

        public ProductsController(IProductsService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                Console.WriteLine("=== API GetAllProducts Called ===");
                var products = await _productService.GetAllAsync();
                Console.WriteLine($"Returning {products?.Count} products");
                return Ok(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== API ERROR ===");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack: {ex.StackTrace}");
                return StatusCode(500, new
                {
                    error = "Internal Server Error",
                    message = ex.Message
                });
            }
        }
    }
}
         

