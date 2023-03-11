using AngularLogin.Models;
using Microsoft.EntityFrameworkCore;
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
            .HasOne(e => e.Customer)
            .WithMany(s => s.Lots)
            .HasForeignKey(e => e.CustomerId);

            modelBuilder.Entity<Lot>()
                .HasOne(e => e.Rep)
                .WithMany(s => s.Lots)
                .HasForeignKey(e => e.RepId);
        }
    }
}
