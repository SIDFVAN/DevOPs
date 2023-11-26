using Blanche.Domain.Customers;
using Blanche.Domain.Products;
using Blanche.Domain.Reservations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Blanche.Domain.Formulas;
using Blanche.Server.Persistence.Configurations.Customers;
using Blanche.Server.Persistence.Configurations.Reservations;
using Blanche.Server.Persistence.Configurations.Formulas;
using Blanche.Server.Persistence.Triggers;
using Blanche.Domain.Invoices;

namespace Blanche.Server
{
	public class BlancheDbContext : DbContext
	{
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<PopularDate> PopularDates { get; set; }

        public BlancheDbContext(DbContextOptions<BlancheDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder
                .EnableSensitiveDataLogging();
            optionsBuilder.UseTriggers(options =>
            {
                options.AddTrigger<EntityBeforeSaveTrigger>();
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new FormulaConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // All decimals should have 2 digits after the comma
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
            // Max Length of a NVARCHAR that can be indexed
            //configurationBuilder.Properties<string>().HaveMaxLength(4_000);
        }
    }
}

