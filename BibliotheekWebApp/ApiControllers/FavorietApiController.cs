using BibliotheekApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class FavorietApiController : ControllerBase
{
    private readonly BibliotheekContext _context;

    public FavorietApiController(BibliotheekContext context)
    {
        _context = context;
    }

    private async Task<int?> GetTestLidIdAsync()
    {
        var lid = await _context.Leden.FirstOrDefaultAsync(l => l.IsDeleted == false);
        return lid?.LidID;
    }

    [HttpGet]
    public async Task<IActionResult> GetFavorieten()
    {
        var lidId = await GetTestLidIdAsync();
        if (lidId == null)
            return BadRequest("❌ Geen geldig lid gevonden in de database.");

        var favorieten = await _context.Favorieten
            .Include(f => f.Boek)
            .Include(f => f.Auteur)
            .Where(f => f.GebruikerId == lidId)
            .ToListAsync();

        return Ok(favorieten);
    }

    [HttpPost("boek/{isbn}")]
    public async Task<IActionResult> VoegFavorietBoekToe(string isbn)
    {
        var lidId = await GetTestLidIdAsync();
        if (lidId == null)
            return BadRequest("❌ Geen geldig lid gevonden in de database.");

        var bestaat = await _context.Favorieten.AnyAsync(f =>
            f.GebruikerId == lidId && f.ISBN == isbn && f.Type == "Boek");

        if (bestaat)
            return BadRequest("Boek al favoriet");

        var favoriet = new Favoriet
        {
            GebruikerId = lidId.Value,
            ISBN = isbn,
            Type = "Boek"
        };

        _context.Favorieten.Add(favoriet);

        try
        {
            var saved = await _context.SaveChangesAsync();

            return Ok(new
            {
                favoriet.GebruikerId,
                favoriet.ISBN,
                SaveChangesResult = saved
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "❌ Fout bij opslaan",
                exceptionMessage = ex.InnerException?.Message ?? ex.Message,
                stackTrace = ex.StackTrace
            });
        }
    }

    [HttpPost("auteur/{auteurId}")]
    public async Task<IActionResult> VoegFavorietAuteurToe(int auteurId)
    {
        var lidId = await GetTestLidIdAsync();
        if (lidId == null)
            return BadRequest("❌ Geen geldig lid gevonden in de database.");

        var bestaat = await _context.Favorieten.AnyAsync(f =>
            f.GebruikerId == lidId && f.AuteurID == auteurId && f.Type == "Auteur");

        if (bestaat)
            return BadRequest("Auteur al favoriet");

        var favoriet = new Favoriet
        {
            GebruikerId = lidId.Value,
            AuteurID = auteurId,
            Type = "Auteur"
        };

        _context.Favorieten.Add(favoriet);
        var saved = await _context.SaveChangesAsync();

        return Ok(new
        {
            favoriet.GebruikerId,
            favoriet.AuteurID,
            SaveChangesResult = saved
        });
    }

    [HttpDelete("boek/{isbn}")]
    public async Task<IActionResult> VerwijderFavorietBoek(string isbn)
    {
        var lidId = await GetTestLidIdAsync();
        if (lidId == null)
            return BadRequest("❌ Geen geldig lid gevonden in de database.");

        var fav = await _context.Favorieten
            .FirstOrDefaultAsync(f => f.GebruikerId == lidId && f.ISBN == isbn && f.Type == "Boek");

        if (fav == null)
            return NotFound();

        _context.Favorieten.Remove(fav);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("auteur/{auteurId}")]
    public async Task<IActionResult> VerwijderFavorietAuteur(int auteurId)
    {
        var lidId = await GetTestLidIdAsync();
        if (lidId == null)
            return BadRequest("❌ Geen geldig lid gevonden in de database.");

        var fav = await _context.Favorieten
            .FirstOrDefaultAsync(f => f.GebruikerId == lidId && f.AuteurID == auteurId && f.Type == "Auteur");

        if (fav == null)
            return NotFound();

        _context.Favorieten.Remove(fav);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
