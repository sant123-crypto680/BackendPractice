using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendPractice.Data;
using BackendPractice.Models;

namespace BackendPractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZonesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ZonesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/zones
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Zone>>> GetZones()
    {
        return await _context.Zones
            .Include(z => z.Users)
            .Include(z => z.Meters)
            .ToListAsync();
    }

    // GET: api/zones/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Zone>> GetZone(int id)
    {
        var zone = await _context.Zones
            .Include(z => z.Users)
            .Include(z => z.Meters)
            .FirstOrDefaultAsync(z => z.Id == id);

        if (zone == null)
        {
            return NotFound();
        }

        return zone;
    }

    // POST: api/zones
    [HttpPost]
    public async Task<ActionResult<Zone>> CreateZone(Zone zone)
    {
        if (await _context.Zones.AnyAsync(z => z.Code == zone.Code))
        {
            return Conflict(new { message = $"A zone with code '{zone.Code}' already exists. Please provide a unique code." });
        }

        _context.Zones.Add(zone);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetZone), new { id = zone.Id }, zone);
    }

    // PUT: api/zones/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateZone(int id, Zone zone)
    {
        if (id != zone.Id)
        {
            return BadRequest("Zone ID mismatch");
        }

        _context.Entry(zone).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Zones.AnyAsync(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/zones/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteZone(int id)
    {
        var zone = await _context.Zones.FindAsync(id);
        if (zone == null)
        {
            return NotFound();
        }

        _context.Zones.Remove(zone);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
