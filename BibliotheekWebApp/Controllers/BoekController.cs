using Microsoft.AspNetCore.Mvc;
using BibliotheekApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

        // Index: Lijst van boeken
        public IActionResult Index()
        {
            var boeken = _context.Boeken
                                 .Include(b => b.Auteur) // Laad de auteur gerelateerd aan elk boek
                                 .Where(b => !b.IsDeleted) // Alleen niet-verwijderde boeken
                                 .ToList();

            foreach (var boek in boeken)
            {
                if (boek.Auteur == null)
                {
                    boek.Auteur = new Auteur { Naam = "Onbekend" }; // Fallback voor ontbrekende auteurs
                }
            }

            return View(boeken);
        }

        // Create: Laad formulier voor nieuw boek
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

        // Edit: Haal gegevens op voor bewerken
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var boek = _context.Boeken
                               .Include(b => b.Auteur)
                               .FirstOrDefault(b => b.ISBN == id && !b.IsDeleted);

            if (boek == null) return NotFound();

            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(boek);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Boek boek)
        {
            if (id != boek.ISBN) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boek);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Boek bijgewerkt!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError($"Fout bij bijwerken boek: {ex.Message}");
                    TempData["ErrorMessage"] = "Er is een fout opgetreden bij het bijwerken van het boek.";
                }
            }

            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(boek);
        }

        // Delete: Verwijder boek
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var boek = _context.Boeken.FirstOrDefault(b => b.ISBN == id);
            if (boek == null) return NotFound();

            boek.IsDeleted = true; // Markeer als verwijderd
            _context.Update(boek);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Boek succesvol verwijderd!";
            return RedirectToAction(nameof(Index));
        }
    }
}
