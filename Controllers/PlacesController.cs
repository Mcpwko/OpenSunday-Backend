using Microsoft.AspNetCore.Cors;
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
    #region PlacesController
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly OpenSundayContext _context;

        public PlacesController(OpenSundayContext context)
        {
            _context = context;
        }
        #endregion

    // GET: api/Places
    #region GetAllPlaces
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Place>>> GetPlaces()
    {
        return await _context.Places.Include(place => place.LocationSet).ThenInclude(location => location.RegionSet)
            .Include(place => place.LocationSet).ThenInclude(location => location.CitySet)
            .Include(place => place.ReviewSet)
            .Include(place => place.TypeSet)
            .Include(place => place.ReportSet).ThenInclude(report => report.UserSet)
            .Include(place => place.CategorySet).ToListAsync();
    }
        #endregion

    // GET: api/Places/5
    #region snippet_GetByID
    [HttpGet("{id}")]
    public async Task<ActionResult<Place>> GetPlace(long id)
    {
        var place = await _context.Places.FindAsync(id);

        if (place == null)
        {
            return NotFound();
        }

        return place;
    }
        #endregion

    // PUT: api/Places/5
    #region snippet_Update
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlace(long id, PlaceForm place)
    {
        if (id != place.idPlace)
        {
            return BadRequest();
        }
        var oldPlace = await _context.Places.FindAsync(id);
        oldPlace.IdCategory = place.idCategory;
        oldPlace.IdType = place.idType;
        oldPlace.IsVerified = place.isVerified;
        oldPlace.IsOpenSpecialDay = place.isOpenSpecialDay;
        oldPlace.IsOpenSunday = place.isOpenSunday;
        oldPlace.Name = place.name;
        oldPlace.Description = place.description;
        oldPlace.Email = place.email;
        oldPlace.Website = place.website;
        oldPlace.PhoneNumber = place.phoneNumber;

        //City
        var city = new City();
        city.Npa = place.zip;
        city.Name = place.city;
        var ctrlCity = new CitiesController(_context);
        await ctrlCity.PostCity(city);
        var cities = await _context.Cities.ToListAsync();
        var insertedCity = cities.Where(x => x.Name == city.Name && x.Npa == city.Npa).First();

        //Location
        var oldLocation = await _context.Locations.FindAsync(oldPlace.IdLocation);
        oldLocation.IdCity = insertedCity.IdCity;
        oldLocation.IdRegion = place.idRegion;
        oldLocation.Lat = place.lat;
        oldLocation.Long = place.Long;
        oldLocation.Address = place.address;
        var ctrlLocation = new LocationsController(_context);
        await ctrlLocation.PutLocation(oldLocation.IdLocation, oldLocation);
        var locations = await _context.Locations.ToListAsync();
        var insertedLocation = locations.Where(x => x.Address == oldLocation.Address && x.Lat == oldLocation.Lat
        && x.Long == oldLocation.Long).First();

        oldPlace.IdLocation = insertedLocation.IdLocation;





        _context.Entry(oldPlace).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PlaceExists(id))
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

    // POST: api/Places
    #region snippet_Create
    [HttpPost]
    public async Task<ActionResult<Place>> PostPlace(Place place)
    {
        // Add creator ID based on the Auth0 User ID found in the JWT token
        place.Creator = User.Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        place.CreateAt = DateTime.Now;

        _context.Places.Add(place);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPlace), new { id = place.IdPlace }, place);
    }
        #endregion

    // POST: api/Places/insert
    #region snippet_Insert
    [HttpPost("insert")]
    public async Task<ActionResult<Place>> InsertPlace(PlaceForm place)
    {

        var city = new City();
        city.Name = place.city;
        city.Npa = place.zip;

        var ctrlCity = new CitiesController(_context);
        await ctrlCity.PostCity(city);
        var cities = await _context.Cities.ToListAsync();
        var insertedCity = cities.Where(x => x.Name == city.Name && x.Npa == city.Npa).First();

        var location = new Location();

        location.IdCity = insertedCity.IdCity;
        location.Lat = place.lat;
        location.Long = place.Long;
        location.Address = place.address;
        location.IdRegion = place.idRegion;
        var ctrlLocation = new LocationsController(_context);
        await ctrlLocation.PostLocation(location);
        var locations = await _context.Locations.ToListAsync();
        var insertLocation = locations.Where(x => x.Address == location.Address
        && x.Lat == location.Lat
        && x.Long == location.Long
        && x.IdRegion == location.IdRegion).First();

        var insertPlace = new Place();

        // Add creator ID based on the Auth0 User ID found in the JWT token
        insertPlace.Creator = User.Claims.First(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        insertPlace.CreateAt = DateTime.Now;
        insertPlace.Name = place.name;
        insertPlace.Description = place.description;
        insertPlace.Email = place.email;
        insertPlace.Website = place.website;
        insertPlace.PhoneNumber = place.phoneNumber;
        insertPlace.IsOpenSunday = place.isOpenSunday;
        insertPlace.IsOpenSpecialDay = place.isOpenSpecialDay;
        insertPlace.IsVerified = place.isVerified;
        insertPlace.IdLocation = insertLocation.IdLocation;
        insertPlace.IdCategory = place.idCategory;
        insertPlace.IdType = place.idType;


        _context.Places.Add(insertPlace);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPlace), new { id = insertPlace.IdPlace }, insertPlace);
    }
        #endregion

    // DELETE: api/Places/5
    #region snippet_Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult<Place>> DeletePlace(long id)
    {
        var place = await _context.Places.FindAsync(id);
        if (place == null)
        {
            return NotFound();
        }

        _context.Places.Remove(place);
        await _context.SaveChangesAsync();

        return place;
    }
        #endregion

    // GET : api/Rating/5
    // Get the average rating of a place
    #region GetAverageRating
    [HttpGet("Rating/{id}")]
    public async Task<double> GetRating(long id)
    {
        var reviews = await _context.Reviews.ToListAsync();
        var count = reviews.Where(x => x.IdPlace == id).Count();

        if (count > 0)
        {
            return reviews.Where(x => x.IdPlace == id).Average(r => r.Rate);
        }
        return 0;
    }
        #endregion

    //GET : api/Verify/5
    //Get the verified Place
    #region VerifyPlace
    [HttpGet("Verify/{id}")]
    public async Task<ActionResult<Place>> VerifyPlace(long id)
        {
            var place = await _context.Places.FindAsync(id);
            if (place != null)
            {
                place.IsVerified = true;
                    

                _context.Entry(place).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return place;
            }
            return NotFound();
            }
    #endregion

        private bool PlaceExists(long id)
    {
      return _context.Places.Any(e => e.IdPlace == id);
    }

    }
}