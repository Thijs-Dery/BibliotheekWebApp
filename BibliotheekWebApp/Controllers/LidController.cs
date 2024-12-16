using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotheekApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace BibliotheekApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Leden")]
    public class LidController : Controller
    {
        private readonly BibliotheekContext _context;

        public LidController(BibliotheekContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var leden = _context.Leden
                                .Where(l => !l.IsDeleted)
                                .ToList();
            return View("~/Views/Leden/Index.cshtml", leden);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View("~/Views/Leden/Create.cshtml");
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var lid = _context.Leden.Find(id);
            if (lid == null || lid.IsDeleted)
            {
                return NotFound();
            }
            return View("~/Views/Leden/Edit.cshtml", lid);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var lid = _context.Leden.Find(id);
            if (lid == null || lid.IsDeleted)
            {
                return NotFound();
            }
            return View("~/Views/Leden/Delete.cshtml", lid);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("BeheerBoeken/{id}")]
        public IActionResult BeheerBoeken(int id)
        {
            var lid = _context.Leden
                .Include(l => l.GeleendeBoeken)
                .ThenInclude(lb => lb.Boek)
                .FirstOrDefault(l => l.LidID == id);

            if (lid == null || lid.IsDeleted)
            {
                return NotFound();
            }

            ViewBag.BeschikbareBoeken = _context.Boeken
                .Where(b => !b.IsDeleted)
                .ToList();

            return View("~/Views/Leden/BeheerBoeken.cshtml", lid);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("VoegBoekToe")]
        [ValidateAntiForgeryToken]
        public IActionResult VoegBoekToe(int lidID, string ISBN)
        {
            // Controleer of het boek al is toegewezen aan het lid
            var bestaandLidBoek = _context.LidBoeken
                .FirstOrDefault(lb => lb.LidID == lidID && lb.ISBN == ISBN);

            if (bestaandLidBoek != null)
            {
                TempData["ErrorMessage"] = "Dit boek is al toegevoegd aan de boekenlijst van dit lid.";
                return RedirectToAction("BeheerBoeken", new { id = lidID });
            }

            // Voeg het boek toe aan het lid
            var lidBoek = new LidBoek
            {
                LidID = lidID,
                ISBN = ISBN,
                UitleenDatum = DateTime.Now,
                InleverDatum = DateTime.Now.AddDays(14)
            };

            _context.LidBoeken.Add(lidBoek);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Boek succesvol toegevoegd!";
            return RedirectToAction("BeheerBoeken", new { id = lidID });
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("VerwijderBoek")]
        [ValidateAntiForgeryToken]
        public IActionResult VerwijderBoek(int lidID, string ISBN)
        {
            var lidBoek = _context.LidBoeken
                .FirstOrDefault(lb => lb.LidID == lidID && lb.ISBN == ISBN);

            if (lidBoek != null)
            {
                _context.LidBoeken.Remove(lidBoek);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Boek succesvol verwijderd!";
            }

            return RedirectToAction("BeheerBoeken", new { id = lidID });
        }
    }
}
