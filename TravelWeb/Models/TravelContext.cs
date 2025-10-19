using Microsoft.EntityFrameworkCore;
using TravelWeb.Models;

namespace TravelWeb.Data
{
    public class TravelContext : DbContext
    {
        public TravelContext(DbContextOptions<TravelContext> options) : base(options) { }

        public DbSet<Tour> Tours { get; set; }
        public DbSet<VirtualTour> VirtualTours { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    }
}
