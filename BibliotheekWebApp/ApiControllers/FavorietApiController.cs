using BibliotheekApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FavorietApiController : ControllerBase
{
    private readonly BibliotheekContext _context;

    public FavorietApiController(BibliotheekContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetFavorieten()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var favorieten = await _context.Favorieten
            .Include(f => f.Boek)
            .Include(f => f.Auteur)
            .Where(f => f.GebruikerId == userId)
            .ToListAsync();

        return Ok(favorieten);
    }

    [HttpPost("boek/{isbn}")]
    public async Task<IActionResult> VoegFavorietBoekToe(string isbn)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (_context.Favorieten.Any(f => f.GebruikerId == userId && f.ISBN == isbn && f.Type == "Boek"))
            return BadRequest("Boek al favoriet");

        _context.Favorieten.Add(new Favoriet
        {
            GebruikerId = userId,
            ISBN = isbn,
            Type = "Boek"
        });

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("auteur/{auteurId}")]
    public async Task<IActionResult> VoegFavorietAuteurToe(int auteurId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (_context.Favorieten.Any(f => f.GebruikerId == userId && f.AuteurID == auteurId && f.Type == "Auteur"))
            return BadRequest("Auteur al favoriet");

        _context.Favorieten.Add(new Favoriet
        {
            GebruikerId = userId,
            AuteurID = auteurId,
            Type = "Auteur"
        });

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("boek/{isbn}")]
    public async Task<IActionResult> VerwijderFavorietBoek(string isbn)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var fav = await _context.Favorieten
            .FirstOrDefaultAsync(f => f.GebruikerId == userId && f.ISBN == isbn && f.Type == "Boek");

        if (fav == null) return NotFound();

        _context.Favorieten.Remove(fav);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("auteur/{auteurId}")]
    public async Task<IActionResult> VerwijderFavorietAuteur(int auteurId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var fav = await _context.Favorieten
            .FirstOrDefaultAsync(f => f.GebruikerId == userId && f.AuteurID == auteurId && f.Type == "Auteur");

        if (fav == null) return NotFound();

        _context.Favorieten.Remove(fav);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
