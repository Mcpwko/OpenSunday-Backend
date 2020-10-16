using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using opensunday_backend.Models;
using OpenSundayApi.Models;
using Type = opensunday_backend.Models.Type;

namespace OpenSundayApi.Controllers
{
  #region TypesController
  [Route("api/[controller]")]
  [ApiController]
  public class TypesController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public TypesController(OpenSundayContext context)
    {
      _context = context;
    }
    #endregion

    // GET: api/Types
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Type>>> GetTypes()
    {
      return await _context.Types.Include(type => type.PlaceSet).ToListAsync();
    }

    #region snippet_GetByID
    // GET: api/Types/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Type>> GetType(long id)
    {
      var type = await _context.Types.FindAsync(id);

      if (type == null)
      {
        return NotFound();
      }

      return type;
    }
    #endregion

    #region snippet_Update
    // PUT: api/Type/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutType(long id, Type type)
    {
      if (id != type.IdType)
      {
        return BadRequest();
      }

      _context.Entry(type).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TypeExists(id))
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
    // POST: api/Types
    [HttpPost]
    public async Task<ActionResult<Type>> PostType(Type type)
    {
      
      _context.Types.Add(type);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetType), new { id = type.IdType }, type);
    }
    #endregion

    #region snippet_Delete
    // DELETE: api/Types/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Type>> DeleteType(long id)
    {
      var type = await _context.Types.FindAsync(id);
      if (type == null)
      {
        return NotFound();
      }

      _context.Types.Remove(type);
      await _context.SaveChangesAsync();

      return type;
    }
    #endregion

    private bool TypeExists(long id)
    {
      return _context.Types.Any(e => e.IdType == id);
    }
  }
}