using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Index()
        {
            var boeken = _context.Boeken.Where(b => !b.IsDeleted).ToList();
            return View(boeken);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Boek boek)
        {
            if (ModelState.IsValid)
            {
                _context.Boeken.Add(boek);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(boek);
        }

        public IActionResult Edit(string id)
        {
            var boek = _context.Boeken.Find(id);
            if (boek == null)
            {
                return NotFound();
            }
            return View(boek);
        }

        [HttpPost]
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
            return View(boek);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var boek = _context.Boeken.Find(id);
            if (boek == null)
            {
                return NotFound();
            }

            boek.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

