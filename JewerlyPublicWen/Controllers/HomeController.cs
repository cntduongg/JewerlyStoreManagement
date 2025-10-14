using Microsoft.AspNetCore.Mvc;

namespace JewerlyPublicWen.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ViewAllProducts()
        {
            return View(); // This will render ViewAllProducts.cshtml
        }
        public ActionResult About()
        {
            return View();
        }
    }
}
