using AngularLogin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularLogin.Context
{
    public class AppDbContext :DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet <User> Users { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rep> Reps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Location>().ToTable("locations");
            modelBuilder.Entity<Lot>()
            .HasOne(e => e.Location)
            .WithMany(s => s.Lots)
            .HasForeignKey(e => e.LotId);

            modelBuilder.Entity<Lot>()
          .HasOne(e => e.Customer)
          .WithMany()
          .HasForeignKey(e => e.LotId);

            modelBuilder.Entity<Lot>()
          .HasOne(e => e.Rep)
          .WithMany()
          .HasForeignKey(e => e.LotId);
        }
    }
}
