using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotheekApp.Models;
using System.Linq;

namespace BibliotheekApp.Controllers
{
    [Route("Uitleen")]
    public class UitleenController : Controller
    {
        private readonly BibliotheekContext _context;

        public UitleenController(BibliotheekContext context)
        {
            _context = context;
        }

        [HttpGet("UitgeleendeBoeken")]
        public IActionResult UitgeleendeBoeken()
        {
            var uitgeleendeBoeken = _context.LidBoeken
                .Include(lb => lb.Lid)
                .Include(lb => lb.Boek)
                .Where(lb => !lb.Boek.IsDeleted)
                .ToList();

            return View("~/Views/Uitleen/Index.cshtml", uitgeleendeBoeken);
        }
    }
}
