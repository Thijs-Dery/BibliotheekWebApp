using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotheekApp.Models;
using System.Linq;

namespace BibliotheekApp.Controllers
{
    public class AuteurController : Controller
    {
        private readonly BibliotheekContext _context;

        public AuteurController(BibliotheekContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var auteurs = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(auteurs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var auteur = _context.Auteurs.Find(id);
            if (auteur == null)
            {
                return NotFound();
            }

            auteur.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
