using Microsoft.EntityFrameworkCore;
using opensunday_backend.Models;
using OpenSundayApi.Controllers;

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
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // mapping many to many relationship
            modelBuilder.Entity<Location>()
                .HasOne(x => x.RegionSet)
                .WithMany(x => x.LocationSet)
                .HasForeignKey(x => x.IdRegion)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Location>()
                .HasOne(x => x.CitySet)
                .WithMany(x => x.LocationSet)
                .HasForeignKey(x => x.IdCity)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(x => x.TypeSet)
                .WithMany(x => x.CategorySet)
                .HasForeignKey(x => x.IdType)
                .OnDelete(DeleteBehavior.ClientNoAction);



        }

    }
}