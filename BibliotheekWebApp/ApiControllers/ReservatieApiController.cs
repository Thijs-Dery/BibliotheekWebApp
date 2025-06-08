using BibliotheekApp.Models;
using BibliotheekWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotheekWebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservatieApiController : ControllerBase
    {
        private readonly BibliotheekContext _context;

        public ReservatieApiController(BibliotheekContext context)
        {
            _context = context;
        }

        // POST: api/reservatieapi
        [HttpPost]
        public async Task<IActionResult> ReserveerBoek([FromBody] Reservatie reservatie)
        {
            var gebruiker = await _context.Leden.FirstOrDefaultAsync(l => !l.IsDeleted);
            if (gebruiker == null)
                return BadRequest("Geen actieve gebruiker gevonden.");

            reservatie.GebruikerId = gebruiker.LidID.ToString();
            reservatie.ReservatieDatum = DateTime.UtcNow;
            reservatie.Verwerkt = false;

            _context.Reservaties.Add(reservatie);
            await _context.SaveChangesAsync();

            return Ok(reservatie);
        }

        // GET: api/reservatieapi/user
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<Reservatie>>> GetGebruikerReservaties()
        {
            var gebruiker = await _context.Leden.FirstOrDefaultAsync(l => !l.IsDeleted);
            if (gebruiker == null)
                return BadRequest("Geen actieve gebruiker gevonden.");

            var reservaties = await _context.Reservaties
                .Where(r => r.GebruikerId == gebruiker.LidID.ToString())
                .ToListAsync();

            return reservaties;
        }

        // DELETE: api/reservatieapi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> VerwijderReservatie(int id)
        {
            var reservatie = await _context.Reservaties.FindAsync(id);
            if (reservatie == null)
                return NotFound("Reservatie niet gevonden.");

            _context.Reservaties.Remove(reservatie);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
