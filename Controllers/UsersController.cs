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
  #region CategoryController
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public CategoryController(OpenSundayContext context)
    {
      _context = context;
    }
    #endregion

    // GET: api/Categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
      return await _context.Categories.ToListAsync();
    }

    #region snippet_GetByID
    // GET: api/Categories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(long id)
    {
      var category = await _context.Categories.FindAsync(id);

      if (category == null)
      {
        return NotFound();
      }

      return category;
    }
    #endregion

    #region snippet_Update
    // PUT: api/Category/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(long id, Category category)
    {
      if (id != category.IdCategory)
      {
        return BadRequest();
      }

      _context.Entry(category).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CategoryExists(id))
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
    // POST: api/Cateogries
    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {

      _context.Categories.Add(category);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetCategory), new { id = category.IdCategory }, category);
    }
    #endregion

    #region snippet_Delete
    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeleteUser(long id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }

      _context.Users.Remove(user);
      await _context.SaveChangesAsync();

      return user;
    }
    #endregion

    private bool CategoryExists(long id)
    {
      return _context.Categories.Any(e => e.IdCategory == id);
    }
  }
}