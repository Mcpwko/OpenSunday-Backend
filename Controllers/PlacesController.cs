using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSundayApi.Models;

namespace OpenSundayApi.Controllers
{
  #region PlacesController
  [Route("api/[controller]")]
  [ApiController]
  public class PlacesController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public PlacesController(OpenSundayContext context)
    {
      _context = context;
    }
    #endregion

    // GET: api/Places
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Place>>> GetPlaces()
    {
      return await _context.Places.ToListAsync();
    }

    #region snippet_GetByID
    // GET: api/Places/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Place>> GetPlace(long id)
    {
      var place = await _context.Places.FindAsync(id);

      if (place == null)
      {
        return NotFound();
      }

      return place;
    }
    #endregion

    #region snippet_Update
    // PUT: api/Place/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlace(long id, Place place)
    {
      if (id != place.IdPlace)
      {
        return BadRequest();
      }

      _context.Entry(place).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PlaceExists(id))
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
    // POST: api/Places
    [HttpPost]
    public async Task<ActionResult<Place>> PostPlace(Place place)
    {
      // Add creator ID based on the Auth0 User ID found in the JWT token
      place.Creator = User.Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

      _context.Places.Add(place);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetPlace), new { id = place.IdPlace }, place);
    }
    #endregion

    #region snippet_Delete
    // DELETE: api/Places/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Place>> DeletePlace(long id)
    {
      var place = await _context.Places.FindAsync(id);
      if (place == null)
      {
        return NotFound();
      }

      _context.Places.Remove(place);
      await _context.SaveChangesAsync();

      return place;
    }
    #endregion

    private bool PlaceExists(long id)
    {
      return _context.Places.Any(e => e.IdPlace == id);
    }
  }
}