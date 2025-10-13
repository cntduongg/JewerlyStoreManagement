using JewerlyPublicWen.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JewerlyPublicWen.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productsService.GetAllProductsAsync();
            return View(products);
        }
    }
}
