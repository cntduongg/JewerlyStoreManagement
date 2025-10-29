using JWPublic.DTO;

using JWPublic.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JWPublic.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ProductService _productService;
        private const int PageSize = 12; // Số sản phẩm mỗi trang

        public List<ProductsDTO> Products { get; set; } = new List<ProductsDTO>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        public ProductsModel(ProductService productService)
        {
            _productService = productService;
        }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            if (pageNumber < 1) pageNumber = 1;

            CurrentPage = pageNumber;

            // Lấy tất cả sản phẩm
            var allProducts = await _productService.GetAllProductsAsync();
            TotalItems = allProducts.Count;
            TotalPages = (int)Math.Ceiling(TotalItems / (double)PageSize);

            // Đảm bảo CurrentPage không vượt quá TotalPages
            if (CurrentPage > TotalPages && TotalPages > 0)
            {
                CurrentPage = TotalPages;
            }

            // Lấy sản phẩm của trang hiện tại
            Products = allProducts
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}
