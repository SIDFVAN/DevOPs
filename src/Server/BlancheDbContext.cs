using Blanche.Domain.Customers;
using Blanche.Domain.Products;
using Blanche.Domain.Reservations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Blanche.Domain.Formulas;
using Blanche.Server.Persistence.Configurations.Customers;
using Blanche.Server.Persistence.Configurations.Reservations;

namespace Blanche.Server
{
	public class BlancheDbContext : DbContext
	{
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<PopularDate> PopularDates { get; set; }

        public BlancheDbContext(DbContextOptions<BlancheDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

