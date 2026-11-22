using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendPractice.Data;
using BackendPractice.Models;

namespace BackendPractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetersController : ControllerBase
{
    private readonly AppDbContext _context;

    public MetersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/meters
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Meter>>> GetMeters()
    {
        return await _context.Meters
            .Include(m => m.Zone)
            .Include(m => m.User)
            .ToListAsync();
    }

    // GET: api/meters/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Meter>> GetMeter(int id)
    {
        var meter = await _context.Meters
            .Include(m => m.Zone)
            .Include(m => m.User)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (meter == null)
        {
            return NotFound();
        }

        return meter;
    }

    // POST: api/meters
    [HttpPost]
    public async Task<ActionResult<Meter>> CreateMeter(Meter meter)
    {
        if (await _context.Meters.AnyAsync(m => m.SerialNumber == meter.SerialNumber))
        {
            return Conflict(new { message = $"A meter with serial number '{meter.SerialNumber}' already exists. Please provide a unique serial number." });
        }

        _context.Meters.Add(meter);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMeter), new { id = meter.Id }, meter);
    }

    // PUT: api/meters/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMeter(int id, Meter meter)
    {
        if (id != meter.Id)
        {
            return BadRequest("Meter ID mismatch");
        }

        _context.Entry(meter).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Meters.AnyAsync(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/meters/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeter(int id)
    {
        var meter = await _context.Meters.FindAsync(id);
        if (meter == null)
        {
            return NotFound();
        }

        _context.Meters.Remove(meter);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
