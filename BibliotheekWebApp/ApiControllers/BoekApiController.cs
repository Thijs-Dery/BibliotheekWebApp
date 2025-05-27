using BibliotheekApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class BoekApiController : ControllerBase
{
    private readonly BibliotheekContext _context;

    public BoekApiController(BibliotheekContext context)
    {
        _context = context;
    }

    // GET: api/BoekApi
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BoekDto>>> GetBoeken()
    {
        var boeken = await _context.Boeken
            .Include(b => b.Auteur)
            .Where(b => !b.IsDeleted)
            .Select(b => new BoekDto
            {
                ISBN = b.ISBN,
                Titel = b.Titel,
                Genre = b.Genre,
                PublicatieDatum = b.PublicatieDatum,
                AuteurNaam = b.Auteur.Naam
            })
            .ToListAsync();

        return boeken;
    }

    // GET: api/BoekApi/{id}
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<BoekDto>> GetBoek(string id)
    {
        var boek = await _context.Boeken
            .Include(b => b.Auteur)
            .Where(b => b.ISBN == id && !b.IsDeleted)
            .Select(b => new BoekDto
            {
                ISBN = b.ISBN,
                Titel = b.Titel,
                Genre = b.Genre,
                PublicatieDatum = b.PublicatieDatum,
                AuteurNaam = b.Auteur.Naam
            })
            .FirstOrDefaultAsync();

        if (boek == null)
            return NotFound();

        return boek;
    }

    // POST: api/BoekApi
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult<Boek>> PostBoek(Boek boek)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Boeken.Add(boek);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBoek), new { id = boek.ISBN }, boek);
    }

    // PUT: api/BoekApi/{id}
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBoek(string id, Boek boek)
    {
        if (id != boek.ISBN)
            return BadRequest("ISBN komt niet overeen.");

        _context.Entry(boek).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Boeken.Any(b => b.ISBN == id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    // DELETE: api/BoekApi/{id}
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBoek(string id)
    {
        var boek = await _context.Boeken.FirstOrDefaultAsync(b => b.ISBN == id);
        if (boek == null || boek.IsDeleted)
            return NotFound();

        boek.IsDeleted = true;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
