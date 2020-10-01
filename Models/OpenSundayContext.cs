using Microsoft.EntityFrameworkCore;

namespace OpenSundayApi.Models
{
    public class OpenSundayContext : DbContext
    {
        public OpenSundayContext(DbContextOptions<OpenSundayContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
    }
}