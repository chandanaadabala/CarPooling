using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.DB.Models
{
    public class CarpoolContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Station> StopOvers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Station> Stations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SANKETH-PC\\CHANDANA;Database=CarPool;User ID=TECHNOVERT\\\\chandana.a;Integrated Security=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .HasName("Email")
                .IsUnique();

            modelBuilder.Entity<Station>()
               .HasIndex(s => s.RideID)
               .HasName("RideID");

            modelBuilder.Entity<Car>()
                .HasIndex(c => c.UserID)
                .HasName("UserID");

            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.RideID)
                .HasName("RideID");

            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.UserID)
                .HasName("UserID");

            modelBuilder.Entity<Ride>()
                .HasIndex(r => r.UserID)
                .HasName("UserID");

            //Data seeding
            modelBuilder.Entity<User>()
                .HasData(new User { ID = "1", Name = "SamYoung", Email = "sam@gmail.com", MobileNum = "0987654321",Password="qazwsxedc"});

        }
    }
}
