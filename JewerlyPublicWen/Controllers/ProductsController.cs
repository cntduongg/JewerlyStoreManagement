using JewerlyPublicWen.Models;
using JewerlyPublicWen.Service.IService;
using Microsoft.AspNetCore.Mvc;
using JewerlyPublicWen.Models;
using JewerlyPublicWen.Models.Dtos;

namespace JewerlyPublicWen.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        // GET: Products/Index - Hiển thị 12 sản phẩm mới nhất
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _productsService.GetAllProductsAsync();
                var latestProducts = products
                    .OrderByDescending(p => p.ProductId)
                    .Take(12)
                    .ToList();

                return View(latestProducts);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể tải sản phẩm: " + ex.Message;
                return View(new List<ProductsViewModel>());
            }
        }

        // GET: Products/ViewAllProducts - Hiển thị tất cả sản phẩm với filter
        public async Task<IActionResult> ViewAllProducts(int? categoryId, string? search, string? sort)
        {
            try
            {
                var products = await _productsService.GetAllProductsAsync();
                var categories = await _productsService.GetAllCategoriesAsync();


                // Lọc theo category
                if (categoryId.HasValue && categoryId.Value > 0)
                {
                    products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
                }

                // Tìm kiếm theo tên
                if (!string.IsNullOrWhiteSpace(search))
                {
                    products = products.Where(p =>
                        p.ProductName.Contains(search, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                // Sắp xếp
                products = sort switch
                {
                    "price_asc" => products.OrderBy(p => p.SellingPrice).ToList(),
                    "price_desc" => products.OrderByDescending(p => p.SellingPrice).ToList(),
                    "name_asc" => products.OrderBy(p => p.ProductName).ToList(),
                    "name_desc" => products.OrderByDescending(p => p.ProductName).ToList(),
                    _ => products.OrderByDescending(p => p.ProductId).ToList() // newest
                };

                ViewBag.CategoryId = categoryId;
                ViewBag.Categories = categories;
                ViewBag.Search = search;
                ViewBag.Sort = sort ?? "newest";

                return View(products);
            }
            catch (Exception ex)
            {
               
               
                TempData["Error"] = "Không thể tải sản phẩm: " + ex.Message;
                return View(new List<ProductsViewModel>());
            }
        }
    }
}