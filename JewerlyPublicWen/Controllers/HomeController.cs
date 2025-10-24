using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public ActionResult ViewAllProducts()
    {
        return RedirectToAction("ViewAllProducts", "Products"); // ← SỬA THÀNH REDIRECT
    }

    public ActionResult About()
    {
        return View();
    }
}