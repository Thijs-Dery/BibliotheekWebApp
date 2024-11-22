using BibliotheekApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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

        // READ: Toon alle auteurs
        public IActionResult Index()
        {
            var auteurs = _context.Auteurs.ToList();
            return View(auteurs);
        }

        // CREATE: Auteur toevoegen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string naam, DateTime geboorteDatum)
        {
            if (ModelState.IsValid)
            {
                var bestaandeAuteur = _context.Auteurs
                    .FirstOrDefault(a => a.Naam == naam && a.GeboorteDatum == geboorteDatum);

                if (bestaandeAuteur != null)
                {
                    ModelState.AddModelError("", "Deze auteur bestaat al.");
                    return View();
                }

                var auteur = new Auteur
                {
                    Naam = naam,
                    GeboorteDatum = geboorteDatum
                };

                _context.Auteurs.Add(auteur);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // UPDATE: Auteur bewerken
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int auteurId, string naam, DateTime geboorteDatum)
        {
            var auteur = _context.Auteurs.Find(auteurId);
            if (auteur == null)
            {
                return NotFound();
            }

            auteur.Naam = naam;
            auteur.GeboorteDatum = geboorteDatum;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE: Auteur verwijderen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int auteurId)
        {
            var auteur = _context.Auteurs.Find(auteurId);
            if (auteur == null)
            {
                return NotFound();
            }

            _context.Auteurs.Remove(auteur);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

