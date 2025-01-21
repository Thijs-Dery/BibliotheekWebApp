using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotheekApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BibliotheekApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuteurApiController : ControllerBase
    {
        private readonly BibliotheekContext _context;

        public AuteurApiController(BibliotheekContext context)
        {
            _context = context;
        }

        // GET: api/AuteurApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auteur>>> GetAuteurs()
        {
            return await _context.Auteurs
                                 .Where(a => !a.IsDeleted)
                                 .ToListAsync();
        }

        // GET: api/AuteurApi/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Auteur>> GetAuteur(int id)
        {
            var auteur = await _context.Auteurs.FirstOrDefaultAsync(a => a.AuteurID == id && !a.IsDeleted);

            if (auteur == null)
            {
                return NotFound();
            }

            return auteur;
        }

        // POST: api/AuteurApi
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Auteur>> CreateAuteur(Auteur auteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Auteurs.Add(auteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuteur), new { id = auteur.AuteurID }, auteur);
        }

        // PUT: api/AuteurApi/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuteur(int id, Auteur auteur)
        {
            if (id != auteur.AuteurID)
            {
                return BadRequest("Auteur ID komt niet overeen.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(auteur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuteurExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/AuteurApi/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuteur(int id)
        {
            var auteur = await _context.Auteurs.FirstOrDefaultAsync(a => a.AuteurID == id);
            if (auteur == null || auteur.IsDeleted)
            {
                return NotFound();
            }

            auteur.IsDeleted = true; // Soft delete
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuteurExists(int id)
        {
            return _context.Auteurs.Any(a => a.AuteurID == id);
        }
    }
}
