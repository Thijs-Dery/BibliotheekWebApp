using Microsoft.AspNetCore.Mvc;

namespace BibliotheekApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}




