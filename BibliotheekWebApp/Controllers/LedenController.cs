using Microsoft.AspNetCore.Mvc;
using BibliotheekApp.Models;
using System.Linq;

namespace BibliotheekApp.Controllers
{
    public class LedenController : Controller
    {
        private readonly BibliotheekContext _context;

        public LedenController(BibliotheekContext context)
        {
            _context = context;
        }

        // Lijst van alle leden
        public IActionResult Index()
        {
            var leden = _context.Leden.ToList();
            return View(leden);
        }

        // Lid toevoegen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string naam, DateTime geboortedatum)
        {
            if (ModelState.IsValid)
            {
                var lid = new Lid
                {
                    Naam = naam,
                    GeboorteDatum = geboortedatum
                };

                _context.Leden.Add(lid);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // Lid bewerken
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string naam, DateTime geboortedatum)
        {
            var lid = _context.Leden.Find(id);
            if (lid == null)
            {
                return NotFound();
            }

            lid.Naam = naam;
            lid.GeboorteDatum = geboortedatum;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Lid verwijderen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var lid = _context.Leden.Find(id);
            if (lid == null)
            {
                return NotFound();
            }

            _context.Leden.Remove(lid);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

