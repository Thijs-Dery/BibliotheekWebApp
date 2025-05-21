using BibliotheekApp.Models;
using BibliotheekWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace BibliotheekWebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservatieApiController : ControllerBase
    {
        private readonly BibliotheekContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservatieApiController(BibliotheekContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: api/reservaties
        [HttpPost]
        public async Task<IActionResult> ReserveerBoek([FromBody] Reservatie reservatie)
        {
            reservatie.ReservatieDatum = DateTime.UtcNow;
            reservatie.Verwerkt = false;
            _context.Reservaties.Add(reservatie);
            await _context.SaveChangesAsync();

            return Ok(reservatie);
        }

        // GET: api/reservaties/user
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<Reservatie>>> GetGebruikerReservaties()
        {
            var gebruikerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.Reservaties
                .Where(r => r.GebruikerId == gebruikerId)
                .ToListAsync();
        }
    }

}
