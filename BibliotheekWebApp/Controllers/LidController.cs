using Microsoft.AspNetCore.Mvc;
using BibliotheekApp.Models;
using System.Linq;

namespace BibliotheekApp.Controllers
{
    public class LidController : Controller
    {
        private readonly BibliotheekContext _context;

        public LidController(BibliotheekContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var leden = _context.Leden.Where(l => !l.IsDeleted).ToList();
            return View(leden);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Lid lid)
        {
            if (ModelState.IsValid)
            {
                _context.Leden.Add(lid);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(lid);
        }

        public IActionResult Edit(int id)
        {
            var lid = _context.Leden.Find(id);
            if (lid == null)
            {
                return NotFound();
            }
            return View(lid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Lid lid)
        {
            if (id != lid.LidID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(lid);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(lid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var lid = _context.Leden.Find(id);
            if (lid == null)
            {
                return NotFound();
            }

            lid.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }

}




