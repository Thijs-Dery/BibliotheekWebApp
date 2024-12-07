using Microsoft.AspNetCore.Mvc;
using BibliotheekApp.Models;
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
            // Haal auteurs op uit de database
            var auteurs = _context.Auteurs.Where(a => !a.IsDeleted).ToList();

            // Controleer of auteurs bestaan, stel een lege lijst in als fallback
            ViewData["Auteurs"] = auteurs.Any() ? auteurs : new List<Auteur>();

            return View(new Boek());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Boek boek, string? IsISBNBlank)
        {
            // Herlaad de auteurs voor validatie en herladen van de view
            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();

            // Valideer de geselecteerde auteur
            if (boek.AuteurID <= 0)
            {
                ModelState.AddModelError("AuteurID", "Selecteer een geldige auteur.");
            }

            // Genereer een ISBN als de checkbox is aangevinkt
            if (!string.IsNullOrEmpty(IsISBNBlank) && IsISBNBlank == "on")
            {
                boek.ISBN = "TEST-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            }

            // Valideer het model en voeg het boek toe
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Boeken.Add(boek);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Boek succesvol toegevoegd!";
                    return RedirectToAction(nameof(Create));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Fout bij het toevoegen van het boek: {ex.Message}";
                }
            }

            return View(boek);
        }
    }
}
