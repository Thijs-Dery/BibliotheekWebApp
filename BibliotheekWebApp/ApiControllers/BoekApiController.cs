using BibliotheekApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Authorize]
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
        {
            return NotFound();
        }

        return boek;
    }

    // POST, PUT, DELETE blijven ongewijzigd
}
