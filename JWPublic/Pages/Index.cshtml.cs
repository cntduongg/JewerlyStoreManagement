using JWPublic.DTO;

using JWPublic.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JWPublic.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;

        public List<ProductsDTO> Products { get; set; } = new List<ProductsDTO>();

        public IndexModel(ProductService productService)
        {
            _productService = productService;
        }

        public async Task OnGetAsync()
        {
            Products = await _productService.GetTop20ProductsAsync();
        }
    }
}