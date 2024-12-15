using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BibliotheekApp.Models;
using System.Linq;

namespace BibliotheekApp.Controllers
{
    [Route("Auteur")]
    public class AuteurController : Controller
    {
        private readonly BibliotheekContext _context;
        private readonly ILogger<AuteurController> _logger;

        public AuteurController(BibliotheekContext context, ILogger<AuteurController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            _logger.LogInformation("Index: Laden van auteurs die niet verwijderd zijn.");
            var auteurs = _context.Auteurs
                                  .Where(a => !a.IsDeleted)
                                  .ToList();
            return View(auteurs);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            _logger.LogInformation("Create GET: Formulier voor nieuwe auteur geladen.");
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Auteur auteur)
        {
            if (ModelState.IsValid)
            {
                _context.Auteurs.Add(auteur);
                _context.SaveChanges();
                _logger.LogInformation("Create POST: Auteur {Naam} succesvol toegevoegd.", auteur.Naam);
                return RedirectToAction(nameof(Index));
            }

            _logger.LogWarning("Create POST: Ongeldige invoer voor nieuwe auteur.");
            return View(auteur);
        }

        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            _logger.LogInformation("Edit GET: Bewerken van auteur met ID {Id}.", id);
            var auteur = _context.Auteurs.Find(id);

            if (auteur == null || auteur.IsDeleted)
            {
                _logger.LogWarning("Edit GET: Auteur met ID {Id} niet gevonden of al verwijderd.", id);
                return NotFound();
            }

            return View(auteur);
        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Auteur auteur)
        {
            if (id != auteur.AuteurID)
            {
                _logger.LogError("Edit POST: Mismatch tussen ID in URL ({Id}) en auteur ID ({AuteurID}).", id, auteur.AuteurID);
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(auteur);
                _context.SaveChanges();
                _logger.LogInformation("Edit POST: Auteur met ID {Id} succesvol bijgewerkt.", id);
                return RedirectToAction(nameof(Index));
            }

            _logger.LogWarning("Edit POST: Ongeldige data ingevoerd voor auteur met ID {Id}.", id);
            return View(auteur);
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Delete GET: Bevestiging verwijderen van auteur met ID {Id}.", id);
            var auteur = _context.Auteurs.Find(id);

            if (auteur == null || auteur.IsDeleted)
            {
                _logger.LogWarning("Delete GET: Auteur niet gevonden of al verwijderd.");
                return NotFound();
            }

            return View(auteur);
        }

        [HttpPost("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _logger.LogInformation("DeleteConfirmed POST: Verwijderen van auteur met ID {Id}.", id);
            var auteur = _context.Auteurs.Find(id);

            if (auteur != null)
            {
                auteur.IsDeleted = true; // Soft delete
                _context.SaveChanges();
                _logger.LogInformation("Auteur met ID {Id} gemarkeerd als verwijderd.", id);
            }
            else
            {
                _logger.LogWarning("Auteur met ID {Id} niet gevonden bij verwijderen.", id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
