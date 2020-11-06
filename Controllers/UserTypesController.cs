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
  #region UserTypesController
  [Route("api/[controller]")]
  [ApiController]
  public class UserTypesController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public UserTypesController(OpenSundayContext context)
    {
      _context = context;
    }
        #endregion

    // GET: api/UserTypes
    #region GetAllUserTypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserType>>> GetUserTypes()
    {
      return await _context.UserTypes.ToListAsync();
    }
        #endregion

    // GET: api/UserTypes/5
    #region snippet_GetByID
    [HttpGet("{id}")]
    public async Task<ActionResult<UserType>> GetUserType(long id)
    {
      var userType = await _context.UserTypes.FindAsync(id);

      if (userType == null)
      {
        return NotFound();
      }

      return userType;
    }
        #endregion

    // POST: api/UserTypes
    #region snippet_Create
    [HttpPost]
    public async Task<ActionResult<UserType>> PostUserTypes(UserType userType)
    {

      _context.UserTypes.Add(userType);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetUserType), new { id = userType.IdUserType }, userType);
    }
    #endregion

    private bool UserTypesExists(long id)
    {
      return _context.UserTypes.Any(e => e.IdUserType == id);
    }
  }
}