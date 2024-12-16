using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotheekApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace BibliotheekApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Uitleen")]
    public class UitleenController : Controller
    {
        private readonly BibliotheekContext _context;

        public UitleenController(BibliotheekContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("")]
        [HttpGet("Index")]
        [HttpGet("UitgeleendeBoeken")]
        public IActionResult Index(string searchTerm)
        {
            var uitgeleendeBoeken = _context.LidBoeken
                .Include(lb => lb.Lid)
                .Include(lb => lb.Boek)
                .Where(lb => !lb.Boek.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                uitgeleendeBoeken = uitgeleendeBoeken.Where(lb =>
                    lb.Boek.Titel.Contains(searchTerm) ||
                    lb.Lid.Naam.Contains(searchTerm));
            }

            return View("~/Views/Uitleen/Index.cshtml", uitgeleendeBoeken.ToList());
        }

    }
}

