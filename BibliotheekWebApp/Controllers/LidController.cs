using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GET: Leden
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var leden = _context.Leden
                                .Where(l => !l.IsDeleted)
                                .ToList();
            return View("~/Views/Leden/Index.cshtml", leden);
        }

        // GET: Leden/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View("~/Views/Leden/Create.cshtml");
        }

        // POST: Leden/Create
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

        // GET: Leden/Edit/{id}
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

        // POST: Leden/Edit/{id}
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

        // GET: Leden/Delete/{id}
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

        // POST: Leden/DeleteConfirmed/{id}
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

        // GET: BeheerBoeken/{id}
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

        // POST: VoegBoekToe
        [HttpPost("VoegBoekToe")]
        [ValidateAntiForgeryToken]
        public IActionResult VoegBoekToe(int lidID, string ISBN)
        {
            var lidBoek = new LidBoek
            {
                LidID = lidID,
                ISBN = ISBN,
                UitleenDatum = DateTime.Now,
                InleverDatum = DateTime.Now.AddDays(14) // Standaard uitleentermijn
            };

            _context.LidBoeken.Add(lidBoek);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Boek succesvol toegevoegd!";
            return RedirectToAction("BeheerBoeken", new { id = lidID });
        }

        // POST: VerwijderBoek
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
