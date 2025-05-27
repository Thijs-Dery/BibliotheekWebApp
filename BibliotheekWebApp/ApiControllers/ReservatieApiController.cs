using BibliotheekApp.Models;
using BibliotheekWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BibliotheekWebApp.ApiControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservatieApiController : ControllerBase
    {
        private readonly BibliotheekContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservatieApiController(BibliotheekContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: api/reservatieapi
        [HttpPost]
        public async Task<IActionResult> ReserveerBoek([FromBody] Reservatie reservatie)
        {
            var gebruikerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(gebruikerId))
                return Unauthorized("Geen geldige gebruiker gevonden.");

            reservatie.GebruikerId = gebruikerId;
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
            var gebruikerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(gebruikerId))
                return Unauthorized("Geen geldige gebruiker gevonden.");

            var reservaties = await _context.Reservaties
                .Where(r => r.GebruikerId == gebruikerId)
                .ToListAsync();

            return reservaties;
        }
    }
}
