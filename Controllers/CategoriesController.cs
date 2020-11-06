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
  #region CategoriesController
  [Route("api/[controller]")]
  [ApiController]
  public class CategoriesController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public CategoriesController(OpenSundayContext context)
    {
      _context = context;
    }
        #endregion

    // GET: api/Categories
    #region GetAllCategories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
      return await _context.Categories.ToListAsync();
    }
        #endregion

    // GET: api/Categories/5
    #region snippet_GetByID
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

    // PUT: api/Category/5
    #region snippet_Update
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

    // POST: api/Cateogries
    #region snippet_Create
    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {

    var categories = await _context.Categories.ToListAsync();
    var categoryExist = await _context.Categories.AnyAsync(x => x.Name == category.Name);

    if (categoryExist != false)
    {
        return categories.Where(x => x.Name == category.Name).First();
    }

            _context.Categories.Add(category);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetCategory), new { id = category.IdCategory }, category);
    }
        #endregion

    // DELETE: api/Categories/5
    #region snippet_Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult<Category>> DeleteCategory(long id)
    {
      var category = await _context.Categories.FindAsync(id);
      if (category == null)
      {
        return NotFound();
      }

      _context.Categories.Remove(category);
      await _context.SaveChangesAsync();

      return category;
    }
    #endregion

    private bool CategoryExists(long id)
    {
      return _context.Categories.Any(e => e.IdCategory == id);
    }
  }
}