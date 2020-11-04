using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using opensunday_backend.Models;
using OpenSundayApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSundayApi.Controllers
{
    #region ReportsController
    [Route("api/[controller]")]
  [ApiController]
  public class ReportsController : ControllerBase
  {
    private readonly OpenSundayContext _context;

    public ReportsController(OpenSundayContext context)
    {
      _context = context;
    }
    #endregion

    // GET: api/Reports
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Report>>> GetReports()
    {
            return await _context.Reports.Include(report => report.PlaceSet)
                .Include(report => report.UserSet).ToListAsync();
    }

        [HttpGet("/Validate/{id}")]
        public async Task<ActionResult<Report>> ValidateReport(long id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                report.Status = false;


                _context.Entry(report).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return report;
            }
            return NotFound();
        }




    #region snippet_GetByID
    // GET: api/Reports/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Report>> GetReport(long id)
    {
      var report = await _context.Reports.FindAsync(id);

      if (report == null)
      {
        return NotFound();
      }

      return report;
    }
    #endregion

    #region snippet_Create
    // POST: api/Reports
    [HttpPost]
    public async Task<ActionResult<Report>> PostReport(Report report)
    {
      report.ReportDate = DateTime.Now;

      _context.Reports.Add(report);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetReport), new { id = report.IdReport }, report);
    }
    #endregion

    #region snippet_Delete
    // DELETE: api/Reports/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Report>> DeleteReport(long id)
    {
      var report = await _context.Reports.FindAsync(id);
      if (report == null)
      {
        return NotFound();
      }

      _context.Reports.Remove(report);
      await _context.SaveChangesAsync();

      return report;
    }
    #endregion


    private bool ReportExists(long id)
    {
      return _context.Reports.Any(e => e.IdReport == id);
    }

    }
}