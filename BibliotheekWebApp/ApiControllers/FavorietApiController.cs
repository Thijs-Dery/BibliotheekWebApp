using BibliotheekApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class FavorietApiController : ControllerBase
{
    private readonly BibliotheekContext _context;

    public FavorietApiController(BibliotheekContext context)
    {
        _context = context;
    }

    private string? GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    [HttpGet]
    public async Task<IActionResult> GetFavorieten()
    {
        var userId = GetUserId();
        if (userId == null)
            return Unauthorized("Gebruiker niet ingelogd");

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
        var userId = GetUserId();
        if (userId == null)
            return Unauthorized();

        if (await _context.Favorieten.AnyAsync(f =>
            f.GebruikerId == userId && f.ISBN == isbn && f.Type == "Boek"))
        {
            return BadRequest("Boek al favoriet");
        }

        var favoriet = new Favoriet
        {
            GebruikerId = userId,
            ISBN = isbn,
            Type = "Boek"
        };

        _context.Favorieten.Add(favoriet);
        await _context.SaveChangesAsync();

        return Ok(favoriet);
    }

    [HttpPost("auteur/{auteurId}")]
    public async Task<IActionResult> VoegFavorietAuteurToe(int auteurId)
    {
        var userId = GetUserId();
        if (userId == null)
            return Unauthorized();

        if (await _context.Favorieten.AnyAsync(f =>
            f.GebruikerId == userId && f.AuteurID == auteurId && f.Type == "Auteur"))
        {
            return BadRequest("Auteur al favoriet");
        }

        var favoriet = new Favoriet
        {
            GebruikerId = userId,
            AuteurID = auteurId,
            Type = "Auteur"
        };

        _context.Favorieten.Add(favoriet);
        await _context.SaveChangesAsync();

        return Ok(favoriet);
    }

    [HttpDelete("boek/{isbn}")]
    public async Task<IActionResult> VerwijderFavorietBoek(string isbn)
    {
        var userId = GetUserId();
        if (userId == null)
            return Unauthorized();

        var fav = await _context.Favorieten
            .FirstOrDefaultAsync(f => f.GebruikerId == userId && f.ISBN == isbn && f.Type == "Boek");

        if (fav == null)
            return NotFound();

        _context.Favorieten.Remove(fav);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("auteur/{auteurId}")]
    public async Task<IActionResult> VerwijderFavorietAuteur(int auteurId)
    {
        var userId = GetUserId();
        if (userId == null)
            return Unauthorized();

        var fav = await _context.Favorieten
            .FirstOrDefaultAsync(f => f.GebruikerId == userId && f.AuteurID == auteurId && f.Type == "Auteur");

        if (fav == null)
            return NotFound();

        _context.Favorieten.Remove(fav);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
