using Microsoft.AspNetCore.Mvc;
using BibliotheekApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace BibliotheekApp.Controllers
{
    public class BoekController : Controller
    {
        private readonly BibliotheekContext _context;
        private readonly ILogger<BoekController> _logger;

        public BoekController(BibliotheekContext context, ILogger<BoekController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var boeken = _context.Boeken
                                 .Include(b => b.Auteur)
                                 .Where(b => !b.IsDeleted)
                                 .ToList();

            return View(boeken);
        }

        public IActionResult Create()
        {
            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(new Boek());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Boek boek)
        {
            if (ModelState.IsValid)
            {
                _context.Boeken.Add(boek);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Boek toegevoegd!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(boek);
        }

        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "Ongeldig boek-ID.";
                return RedirectToAction(nameof(Index));
            }

            var boek = _context.Boeken
                               .Include(b => b.Auteur)
                               .FirstOrDefault(b => b.ISBN == id && !b.IsDeleted);

            if (boek == null)
            {
                TempData["ErrorMessage"] = "Boek niet gevonden.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(boek);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Boek boek)
        {
            if (id != boek.ISBN)
            {
                TempData["ErrorMessage"] = "Ongeldig boek-ID.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                _context.Update(boek);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Boek bijgewerkt!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(boek);
        }

        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "Ongeldig boek-ID.";
                return RedirectToAction(nameof(Index));
            }

            var boek = _context.Boeken.Include(b => b.Auteur).FirstOrDefault(b => b.ISBN == id);

            if (boek == null)
            {
                TempData["ErrorMessage"] = "Boek niet gevonden.";
                return RedirectToAction(nameof(Index));
            }

            return View(boek);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "Ongeldig boek-ID.";
                return RedirectToAction(nameof(Index));
            }

            var boek = _context.Boeken.FirstOrDefault(b => b.ISBN == id);

            if (boek == null)
            {
                TempData["ErrorMessage"] = "Boek niet gevonden.";
                return RedirectToAction(nameof(Index));
            }

            boek.IsDeleted = true;
            _context.Update(boek);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Boek verwijderd!";
            return RedirectToAction(nameof(Index));
        }
    }
}
