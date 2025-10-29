using JWPublic.DTO;
using JWPublic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JWPublic.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly ProductService _productService;

        public ProductsDTO? Product { get; set; }

        public ProductDetailModel(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var allProducts = await _productService.GetAllProductsAsync();
            Product = allProducts.FirstOrDefault(p => p.Productid == id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}