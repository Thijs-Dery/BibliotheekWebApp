using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BibliotheekApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public IActionResult SetLanguage(string culture)
        {
            var supportedCultures = new[] { "nl", "en", "fr" };
            if (!supportedCultures.Contains(culture))
            {
                return BadRequest("Unsupported culture.");
            }

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) }
            );

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
