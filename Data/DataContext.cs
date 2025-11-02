

using deliverySystem_Sharqiya.Models;
using Microsoft.EntityFrameworkCore;

namespace deliverySystem_Sharqiya.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global query filter for the Course entity

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DailyDistanceTracker> DailyDistanceTrackers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<DriverToVehicle> DriverToVehicles { get; set; }
    }

}
