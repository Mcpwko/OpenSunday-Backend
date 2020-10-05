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
  #region CitiesController
  [Route("api/[controller]")]
  [ApiController]
  public class CitiesController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public CitiesController(OpenSundayContext context)
    {
      _context = context;
    }
    #endregion

    // GET: api/Cities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetCities()
    {
      return await _context.Cities.ToListAsync();
    }

    #region snippet_GetByID
    // GET: api/Cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<City>> GetCity(long id)
    {
      var city = await _context.Cities.FindAsync(id);

      if (city == null)
      {
        return NotFound();
      }

      return city;
    }
    #endregion

    #region snippet_Update
    // PUT: api/City/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCity(long id, City city)
    {
      if (id != city.IdCity)
      {
        return BadRequest();
      }

      _context.Entry(city).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CityExists(id))
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
    // POST: api/Cities
    [HttpPost]
    public async Task<ActionResult<City>> PostCity(City city)
    {
      
      _context.Cities.Add(city);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetCity), new { id = city.IdCity }, city);
    }
    #endregion

    #region snippet_Delete
    // DELETE: api/Cities/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<City>> DeleteCity(long id)
    {
      var city = await _context.Cities.FindAsync(id);
      if (city == null)
      {
        return NotFound();
      }

      _context.Cities.Remove(city);
      await _context.SaveChangesAsync();

      return city;
    }
    #endregion

    private bool CityExists(long id)
    {
      return _context.Cities.Any(e => e.IdCity == id);
    }
  }
}