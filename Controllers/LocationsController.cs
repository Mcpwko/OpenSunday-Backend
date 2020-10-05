using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using opensunday_backend.Models;
using OpenSundayApi.Models;

namespace OpenSundayApi.Controllers
{
  #region LocationsController
  [Route("api/[controller]")]
  [ApiController]
  public class LocationsController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public LocationsController(OpenSundayContext context)
    {
      _context = context;
    }
    #endregion

    // GET: api/Locations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
    {
      return await _context.Locations.ToListAsync();
    }

    #region snippet_GetByID
    // GET: api/Locations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Location>> GetLocation(long id)
    {
      var location = await _context.Locations.FindAsync(id);

      if (location == null)
      {
        return NotFound();
      }

      return location;
    }
    #endregion

    #region snippet_Update
    // PUT: api/Location/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLocation(long id, Location location)
    {
      if (id != location.IdLocation)
      {
        return BadRequest();
      }

      _context.Entry(location).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!LocationExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }
    #endregion

    #region snippet_Create
    // POST: api/Locations
    [HttpPost]
    public async Task<ActionResult<Location>> PostLocation(Location location)
    {
      
      _context.Locations.Add(location);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetLocation), new { id = location.IdLocation }, location);
    }
    #endregion

    #region snippet_Delete
    // DELETE: api/Locations/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Location>> DeleteLocation(long id)
    {
      var location = await _context.Locations.FindAsync(id);
      if (location == null)
      {
        return NotFound();
      }

      _context.Locations.Remove(location);
      await _context.SaveChangesAsync();

      return location;
    }
    #endregion

    private bool LocationExists(long id)
    {
      return _context.Locations.Any(e => e.IdLocation == id);
    }
  }
}