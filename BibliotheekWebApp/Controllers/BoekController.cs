using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotheekApp.Models;
using System;
using System.Linq;

namespace BibliotheekApp.Controllers
{
    public class BoekController : Controller
    {
        private readonly BibliotheekContext _context;

        public BoekController(BibliotheekContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(new Boek());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Boek boek, string? IsISBNBlank)
        {
            // Voeg auteurs opnieuw toe aan ViewData voor herladen bij fout
            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();

            // Controleer op een geldige auteur
            if (boek.AuteurID <= 0)
            {
                ModelState.AddModelError("AuteurID", "Selecteer een geldige auteur.");
            }

            // Genereer een test-ISBN als de checkbox is aangevinkt
            if (!string.IsNullOrEmpty(IsISBNBlank) && IsISBNBlank == "on")
            {
                boek.ISBN = Guid.NewGuid().ToString("N").Substring(0, 13);
            }

            // Als ModelState geldig is, sla het boek op
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Boeken.Add(boek);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Boek succesvol toegevoegd!";
                    return RedirectToAction(nameof(Create)); // Stuur gebruiker terug naar Create-pagina
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Fout bij het toevoegen van het boek: {ex.Message}";
                }
            }

            // Toon validatiefouten in de UI
            return View(boek);
        }
    }
}

