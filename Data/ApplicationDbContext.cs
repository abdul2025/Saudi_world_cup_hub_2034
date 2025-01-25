using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaudiWorldCupHub.Models;

namespace SaudiWorldCupHub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder); // Don't forget to call this to ensure Identity's model is configured

            }

    
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Nationalitis> Nationalitis { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Traveler> Traveler { get; set; }
    }
}
