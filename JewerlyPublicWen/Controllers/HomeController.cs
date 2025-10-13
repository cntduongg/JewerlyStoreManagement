using Microsoft.AspNetCore.Mvc;

namespace JewerlyPublicWen.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
