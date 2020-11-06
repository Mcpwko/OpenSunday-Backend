using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    #region GetAllReviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
    {
      return await _context.Reviews.Include(review => review.PlaceSet)
                .Include(review => review.UserSet).ToListAsync();
    }
        #endregion

    // GET: api/Reviews/5
    #region snippet_GetByID
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

    // PUT: api/Reviews/5
    #region snippet_Update
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

    // POST: api/Reviews
    #region snippet_Create
    [HttpPost]
    public async Task<ActionResult<Review>> PostReview(Review review)
    {
      
      _context.Reviews.Add(review);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetReview), new { id = review.IdReview }, review);
    }
        #endregion

    // DELETE: api/Reviews/5
    #region snippet_Delete
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

    // GET : api/Reviews/place/5
    #region GetAllReviewsForaPlace
    [HttpGet("place/{id}")]
    public async Task<IEnumerable<Review>> GetReviewsFromPlace(long id)
    {
        var reviews = await _context.Reviews.Include(x =>x.UserSet).ToListAsync();
        return reviews.Where(x => x.IdPlace == id);
    }
    #endregion

    private bool ReviewExists(long id)
    {
      return _context.Reviews.Any(e => e.IdReview == id);
    }
  }
}