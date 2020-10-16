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
  #region RegionsController
  [Route("api/[controller]")]
  [ApiController]
  public class RegionsController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public RegionsController(OpenSundayContext context)
    {
      _context = context;
    }
    #endregion

    // GET: api/Regions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
    {
      return await _context.Regions.Include(region =>region.LocationSet).ToListAsync();
    }

    #region snippet_GetByID
    // GET: api/Regions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Region>> GetRegion(long id)
    {
      var region = await _context.Regions.FindAsync(id);

      if (region == null)
      {
        return NotFound();
      }

      return region;
    }
    #endregion

    #region snippet_Update
    // PUT: api/Region/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegion(long id, Region region)
    {
      if (id != region.IdRegion)
      {
        return BadRequest();
      }

      _context.Entry(region).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!RegionExists(id))
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
    // POST: api/Regions
    [HttpPost]
    public async Task<ActionResult<Region>> PostRegion(Region region)
    {
      
      _context.Regions.Add(region);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetRegion), new { id = region.IdRegion }, region);
    }
    #endregion

    #region snippet_Delete
    // DELETE: api/Regions/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Region>> DeleteRegion(long id)
    {
      var region = await _context.Regions.FindAsync(id);
      if (region == null)
      {
        return NotFound();
      }

      _context.Regions.Remove(region);
      await _context.SaveChangesAsync();

      return region;
    }
    #endregion

    private bool RegionExists(long id)
    {
      return _context.Regions.Any(e => e.IdRegion == id);
    }
  }
}