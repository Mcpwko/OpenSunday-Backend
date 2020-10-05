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
  #region ReviewsController
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public ReviewsController(OpenSundayContext context)
    {
      _context = context;
    }
    #endregion

    // GET: api/Reviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
    {
      return await _context.Reviews.ToListAsync();
    }

    #region snippet_GetByID
    // GET: api/Reviews/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Review>> GetReview(long id)
    {
      var review = await _context.Reviews.FindAsync(id);

      if (review == null)
      {
        return NotFound();
      }

      return review;
    }
    #endregion

    #region snippet_Update
    // PUT: api/Review/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReview(long id, Review review)
    {
      if (id != review.IdReview)
      {
        return BadRequest();
      }

      _context.Entry(review).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ReviewExists(id))
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
    // POST: api/Reviews
    [HttpPost]
    public async Task<ActionResult<Review>> PostReview(Review review)
    {
      
      _context.Reviews.Add(review);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetReview), new { id = review.IdReview }, review);
    }
    #endregion

    #region snippet_Delete
    // DELETE: api/Reviews/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Review>> DeleteReview(long id)
    {
      var review = await _context.Reviews.FindAsync(id);
      if (review == null)
      {
        return NotFound();
      }

      _context.Reviews.Remove(review);
      await _context.SaveChangesAsync();

      return review;
    }
    #endregion

    private bool ReviewExists(long id)
    {
      return _context.Reviews.Any(e => e.IdReview == id);
    }
  }
}