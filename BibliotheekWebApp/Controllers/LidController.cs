using Microsoft.AspNetCore.Mvc;
using BibliotheekApp.Models;
using System.Linq;

namespace BibliotheekApp.Controllers
{
    [Route("Leden")]
    public class LidController : Controller
    {
        private readonly BibliotheekContext _context;

        public LidController(BibliotheekContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var leden = _context.Leden.Where(l => !l.IsDeleted).ToList();
            return View("~/Views/Leden/Index.cshtml", leden);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View("~/Views/Leden/Create.cshtml");
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Lid lid)
        {
            if (ModelState.IsValid)
            {
                _context.Leden.Add(lid);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Lid succesvol toegevoegd!";
                return RedirectToAction(nameof(Index));
            }

            return View("~/Views/Leden/Create.cshtml", lid);
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var lid = _context.Leden.Find(id);
            if (lid == null)
            {
                return NotFound();
            }
            return View("~/Views/Leden/Edit.cshtml", lid);
        }

        [HttpPost("Edit/{id}")]
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
                TempData["SuccessMessage"] = "Lid succesvol bijgewerkt!";
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Leden/Edit.cshtml", lid);
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var lid = _context.Leden.Find(id);
            if (lid == null)
            {
                return NotFound();
            }
            return View("~/Views/Leden/Delete.cshtml", lid);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var lid = _context.Leden.Find(id);
            if (lid != null)
            {
                lid.IsDeleted = true; // Soft delete
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Lid succesvol verwijderd!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}


