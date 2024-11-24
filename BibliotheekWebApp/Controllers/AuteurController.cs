using Microsoft.AspNetCore.Mvc;
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
            var auteurs = _context.Auteurs.ToList();
            return View(auteurs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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

        public IActionResult Edit(int id)
        {
            var auteur = _context.Auteurs.Find(id);
            if (auteur == null)
            {
                return NotFound();
            }
            return View(auteur);
        }

        [HttpPost]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var auteur = _context.Auteurs.Find(id);
            if (auteur == null)
            {
                return NotFound();
            }

            _context.Auteurs.Remove(auteur);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }

}

