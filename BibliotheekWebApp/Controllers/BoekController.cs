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

        // Index view: Toon lijst van boeken
        public IActionResult Index()
        {
            _logger.LogInformation("Navigated to Index view for Boek.");

            // Haal boeken op met de gerelateerde Auteur-entiteit
            var boeken = _context.Boeken
                                 .Include(b => b.Auteur) // Eager Loading van Auteur
                                 .Where(b => !b.IsDeleted)
                                 .ToList();

            // Log de boeken en auteurs
            foreach (var boek in boeken)
            {
                _logger.LogInformation($"Boek: {boek.Titel}, Auteur: {(boek.Auteur != null ? boek.Auteur.Naam : "Geen auteur")}");
            }

            return View(boeken);
        }

        // Create view: Formulier om een boek toe te voegen
        public IActionResult Create()
        {
            _logger.LogInformation("Navigated to Create view for Boek.");
            var auteurs = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            _logger.LogInformation($"Aantal auteurs opgehaald: {auteurs.Count}");

            ViewData["Auteurs"] = auteurs.Any() ? auteurs : new List<Auteur>();
            return View(new Boek());
        }

        // Create POST: Verwerk het formulier
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Boek boek, string? IsISBNBlank)
        {
            _logger.LogInformation("Create POST action triggered.");
            _logger.LogInformation($"Ontvangen Boek Model: {boek.Titel}, AuteurID: {boek.AuteurID}");

            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();

            if (boek.AuteurID <= 0)
            {
                _logger.LogWarning("AuteurID is ongeldig. Validatie mislukt.");
                ModelState.AddModelError("AuteurID", "Selecteer een geldige auteur.");
            }

            if (!string.IsNullOrEmpty(IsISBNBlank) && IsISBNBlank == "on")
            {
                boek.ISBN = "TEST-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
                _logger.LogInformation($"Genereerde TEST-ISBN: {boek.ISBN}");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Model is valid. Poging tot toevoegen van boek aan de database.");
                    _context.Boeken.Add(boek);
                    _context.SaveChanges();
                    _logger.LogInformation("Boek succesvol toegevoegd aan de database.");
                    TempData["SuccessMessage"] = "Boek succesvol toegevoegd!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Fout bij het toevoegen van boek: {ex.Message}");
                    TempData["ErrorMessage"] = $"Fout bij het toevoegen van het boek: {ex.Message}";
                }
            }
            else
            {
                _logger.LogWarning("ModelState is niet geldig. Boek niet toegevoegd.");
            }

            return View(boek);
        }
    }
}
