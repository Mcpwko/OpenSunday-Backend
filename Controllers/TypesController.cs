using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Schema;
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
    #region GetAllTypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Type>>> GetTypes()
    {
      return await _context.Types.Include(type => type.PlaceSet).ToListAsync();
    }
        #endregion

    // GET: api/Types/5
    #region snippet_GetByID
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

    // PUT: api/Type/5
    #region snippet_Update
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

    // POST: api/Types
    #region snippet_Create
    [HttpPost]
    public async Task<ActionResult<Type>> PostType(Type type)
    {
            var types = await _context.Types.ToListAsync();
            var typeExist = await _context.Types.AnyAsync(x => x.Name == type.Name);

        if (typeExist != false)
        {
                return types.Where(x => x.Name == type.Name).First();
        }

        _context.Types.Add(type);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetType), new { id = type.IdType }, type);
    }
        #endregion

    // DELETE: api/Types/5
    #region snippet_Delete
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