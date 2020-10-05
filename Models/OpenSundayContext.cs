using Microsoft.EntityFrameworkCore;
using opensunday_backend.Models;

namespace OpenSundayApi.Models
{
    public class OpenSundayContext : DbContext
    {
        public OpenSundayContext(DbContextOptions<OpenSundayContext> options) : base(options) { }

        public DbSet<Place> Places { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}