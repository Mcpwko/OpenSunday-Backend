using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using opensunday_backend.Models;
using OpenSundayApi.Models;

namespace OpenSundayApi.Controllers
{
  #region UsersController
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public UsersController(OpenSundayContext context)
    {
      _context = context;
    }
    #endregion

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
      return await _context.Users.Include(user => user.UserTypeSet).ToListAsync();
    }


    #region checkPseudo
    // GET: api/Users/Check
    [HttpGet("Check/{nickname}")]
    public async Task<double> CheckPseudo(string nickname)
    {
        var users = await _context.Users.ToListAsync();
        foreach(var user in users)
            {
                if (user.Pseudo!=null && user.Pseudo.Equals(nickname))
                {
                    return 1;
                }
            }
        return 0;
    }
#endregion


    #region snippet_GetByID
    // GET: api/Users/email
    [HttpGet("{email}")]
    public async Task<ActionResult<User>> GetUser(string email)
    {
      var users = await _context.Users.Include(user => user.ReviewSet).ToListAsync();
      var user = users.Where(x => x.Email.Equals(email));

      if (user == null)
      {
        return NotFound();
      }

      return user.First();
    }
    #endregion

        #region snippet_Update
        // PUT: api/User/5
        [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(long id, User user)
    {
      if (id != user.IdUser)
      {
        return BadRequest();
      }

      _context.Entry(user).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserExists(id))
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
    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {

    var userExist = await _context.Users.AnyAsync(x => x.IdAuth0 == user.IdAuth0);
    var users = await _context.Users.ToListAsync();

    if (userExist != false)
    {
        return users.Where(x => x.IdAuth0 == user.IdAuth0).First();
    }

      user.CreatedAt = DateTime.Now;
      user.Status = 0;

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetUser), new { id = user.IdUser }, user);
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

    private bool UserExists(long id)
    {
      return _context.Users.Any(e => e.IdUser == id);
    }
  }
}