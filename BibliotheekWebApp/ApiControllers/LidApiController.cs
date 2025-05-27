using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotheekApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BibliotheekApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LidApiController : ControllerBase
    {
        private readonly BibliotheekContext _context;

        public LidApiController(BibliotheekContext context)
        {
            _context = context;
        }

        // GET: api/LidApi
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Lid>>> GetLeden()
        {
            return await _context.Leden
                                 .Where(l => !l.IsDeleted)
                                 .ToListAsync();
        }

        // GET: api/LidApi/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Lid>> GetLid(int id)
        {
            var lid = await _context.Leden
                                    .Include(l => l.GeleendeBoeken)
                                    .ThenInclude(lb => lb.Boek)
                                    .FirstOrDefaultAsync(l => l.LidID == id && !l.IsDeleted);

            if (lid == null)
                return NotFound();

            return lid;
        }

        // POST: api/LidApi
        [HttpPost]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Lid>> CreateLid(Lid lid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Leden.Add(lid);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLid), new { id = lid.LidID }, lid);
        }

        // PUT: api/LidApi/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateLid(int id, Lid lid)
        {
            if (id != lid.LidID)
                return BadRequest("Lid ID komt niet overeen.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(lid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LidExists(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/LidApi/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteLid(int id)
        {
            var lid = await _context.Leden.FirstOrDefaultAsync(l => l.LidID == id);
            if (lid == null || lid.IsDeleted)
                return NotFound();

            lid.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/LidApi/VoegBoekToe
        [HttpPost("VoegBoekToe")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> VoegBoekToe(int lidID, string ISBN)
        {
            var bestaandLidBoek = await _context.LidBoeken
                                                .FirstOrDefaultAsync(lb => lb.LidID == lidID && lb.ISBN == ISBN);

            if (bestaandLidBoek != null)
                return BadRequest("Boek is al toegevoegd aan dit lid.");

            var lidBoek = new LidBoek
            {
                LidID = lidID,
                ISBN = ISBN,
                UitleenDatum = DateTime.Now,
                InleverDatum = DateTime.Now.AddDays(14)
            };

            _context.LidBoeken.Add(lidBoek);
            await _context.SaveChangesAsync();

            return Ok("Boek toegevoegd.");
        }

        // POST: api/LidApi/VerwijderBoek
        [HttpPost("VerwijderBoek")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> VerwijderBoek(int lidID, string ISBN)
        {
            var lidBoek = await _context.LidBoeken
                                        .FirstOrDefaultAsync(lb => lb.LidID == lidID && lb.ISBN == ISBN);

            if (lidBoek == null)
                return NotFound();

            _context.LidBoeken.Remove(lidBoek);
            await _context.SaveChangesAsync();

            return Ok("Boek verwijderd.");
        }

        private bool LidExists(int id)
        {
            return _context.Leden.Any(l => l.LidID == id);
        }
    }
}
