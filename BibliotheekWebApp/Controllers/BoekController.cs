using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotheekApp.Models;
using System;
using System.Linq;

namespace BibliotheekApp.Controllers
{
    [Authorize(Roles = "Admin")] // Alleen toegankelijk voor Admin-rollen
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
            else if (string.IsNullOrEmpty(boek.ISBN) || boek.ISBN.Length != 13)
            {
                ModelState.AddModelError("ISBN", "ISBN moet precies 13 tekens bevatten.");
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
                catch (DbUpdateException ex)
                {
                    TempData["ErrorMessage"] = $"Fout bij het opslaan in de database: {ex.Message}";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Onverwachte fout: {ex.Message}";
                }
            }

            // Toon validatiefouten in de UI
            return View(boek);
        }
    }
}
