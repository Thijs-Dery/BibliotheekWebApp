using BibliotheekApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // Lijst van alle boeken
        public IActionResult Index()
        {
            var boeken = _context.Boeken
                .Include(b => b.Auteur)
                .ToList();
            return View(boeken);
        }

        // Boek toevoegen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string titel, string genre, DateTime? publicatieDatum, int? auteurId, string isbn)
        {
            if (ModelState.IsValid)
            {
                if (auteurId == null || string.IsNullOrWhiteSpace(titel) || string.IsNullOrWhiteSpace(genre) || !publicatieDatum.HasValue)
                {
                    ModelState.AddModelError("", "Alle velden moeten ingevuld zijn.");
                    return View();
                }

                var boek = new Boek
                {
                    ISBN = isbn,
                    Titel = titel,
                    Genre = genre,
                    PublicatieDatum = publicatieDatum.Value,
                    AuteurID = auteurId.Value
                };

                _context.Boeken.Add(boek);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // Boek bewerken
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string titel, string genre, DateTime publicatieDatum)
        {
            var boek = _context.Boeken.Find(id);
            if (boek == null)
            {
                return NotFound();
            }

            boek.Titel = titel;
            boek.Genre = genre;
            boek.PublicatieDatum = publicatieDatum;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Boek verwijderen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var boek = _context.Boeken.Find(id);
            if (boek == null)
            {
                return NotFound();
            }

            _context.Boeken.Remove(boek);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
