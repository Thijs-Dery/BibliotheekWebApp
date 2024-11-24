using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotheekApp.Models;
using System.Linq;

namespace BibliotheekApp.Controllers
{
    public class LidController : Controller
    {
        private readonly BibliotheekContext _context;

        public LidController(BibliotheekContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var leden = _context.Leden.Where(l => !l.IsDeleted).ToList();
            return View(leden);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var lid = _context.Leden.Find(id);
            if (lid == null)
            {
                return NotFound();
            }

            lid.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}


