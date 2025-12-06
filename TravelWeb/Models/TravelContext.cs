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
<<<<<<< Updated upstream
=======
        public DbSet<Location> Locations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<ActivityHistory> ActivityHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
>>>>>>> Stashed changes

            // Configure Review
            modelBuilder.Entity<Review>()
                .HasIndex(r => r.CuisineId);
            modelBuilder.Entity<Review>()
                .HasIndex(r => r.UserId);
            modelBuilder.Entity<Review>()
                .HasIndex(r => r.CreatedAt);

            // Configure Favorite
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.UserId, f.ItemId, f.Type })
                .IsUnique();
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => f.UserId);
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => f.CreatedAt);

            // Configure ActivityHistory
            modelBuilder.Entity<ActivityHistory>()
                .HasIndex(a => a.UserId);
            modelBuilder.Entity<ActivityHistory>()
                .HasIndex(a => a.CreatedAt);
            modelBuilder.Entity<ActivityHistory>()
                .HasIndex(a => a.ActivityType);
        }
    }
}
