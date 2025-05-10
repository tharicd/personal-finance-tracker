using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PersonalFinanceTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FinanceEntryController : ControllerBase
{
    private readonly AppDbContext _context;

    public FinanceEntryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FinanceEntry>>> Get()
    {
        try
        {
            return await _context.FinanceEntries.OrderByDescending(o=>o.Date).ToListAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
        
    }
        

    [HttpPost]
    public async Task<IActionResult> Post(FinanceEntry entry)
    {
        try
        {
            if (entry.Amount <= 0 || entry.Date > DateTime.Today)
            {
                return BadRequest("Invalid amount or future date.");
            }
                
            _context.FinanceEntries.Add(entry);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(Get), new { id = entry.Id }, entry);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, FinanceEntry updatedEntry)
    {
        try
        {
            if (updatedEntry.Amount <= 0 || updatedEntry.Date > DateTime.Today)
            {
                return BadRequest("Invalid update: check amount and date.");
            }

            var entry = await _context.FinanceEntries.FindAsync(id);
            if (entry == null)
            {
                return NotFound($"Entry with ID {id} not found.");
            }
            entry.Date = updatedEntry.Date;
            entry.Description = updatedEntry.Description;
            entry.Amount = updatedEntry.Amount;
            entry.Type = updatedEntry.Type;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}"); ;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var entry = await _context.FinanceEntries.FindAsync(id);
            if (entry == null)
            {
                return NotFound($"Entry with ID {id} not found.");
            }

            _context.FinanceEntries.Remove(entry);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
        
    }

    //not requested
    [HttpGet("{id}")]
    public async Task<ActionResult<FinanceEntry>> GetEntry(int id)
    {
        try
        {
            var entry = await _context.FinanceEntries.FindAsync(id);
            if (entry == null) return NotFound($"Entry with ID {id} not found.");
            return Ok(entry);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}