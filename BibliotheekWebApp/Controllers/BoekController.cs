using Microsoft.AspNetCore.Mvc;
using BibliotheekApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotheekApp.Controllers
{
    [Route("Boek")]
    public class BoekController : Controller
    {
        private readonly BibliotheekContext _context;

        public BoekController(BibliotheekContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var boeken = _context.Boeken
                                 .Include(b => b.Auteur)
                                 .Where(b => !b.IsDeleted)
                                 .ToList();
            return View(boeken);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View();
        }

        [HttpPost("Create")]
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

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(string id)
        {
            var boek = _context.Boeken.Include(b => b.Auteur).FirstOrDefault(b => b.ISBN == id);
            if (boek == null)
            {
                return NotFound();
            }
            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(boek);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Boek boek)
        {
            if (id != boek.ISBN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(boek);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Auteurs"] = _context.Auteurs.Where(a => !a.IsDeleted).ToList();
            return View(boek);
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var boek = _context.Boeken.FirstOrDefault(b => b.ISBN == id);
            if (boek == null)
            {
                return NotFound();
            }

            return View(boek);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var boek = _context.Boeken.FirstOrDefault(b => b.ISBN == id);
            if (boek != null)
            {
                boek.IsDeleted = true;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
