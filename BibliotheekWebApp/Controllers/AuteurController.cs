using Microsoft.AspNetCore.Mvc;
using BibliotheekApp.Models;
using System.Linq;

namespace BibliotheekApp.Controllers
{
    [Route("Auteur")]
    public class AuteurController : Controller
    {
        private readonly BibliotheekContext _context;

        public AuteurController(BibliotheekContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var auteurs = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(auteurs);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Auteur auteur)
        {
            if (ModelState.IsValid)
            {
                _context.Auteurs.Add(auteur);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(auteur);
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var auteur = _context.Auteurs.Find(id);
            if (auteur == null || auteur.IsDeleted)
            {
                return NotFound();
            }

            return View(auteur);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Auteur auteur)
        {
            if (id != auteur.AuteurID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(auteur);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(auteur);
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var auteur = _context.Auteurs.Find(id);
            if (auteur == null || auteur.IsDeleted)
            {
                return NotFound();
            }

            return View(auteur);
        }

        [HttpPost("DeleteConfirmed/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var auteur = _context.Auteurs.Find(id);
            if (auteur != null)
            {
                auteur.IsDeleted = true;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

