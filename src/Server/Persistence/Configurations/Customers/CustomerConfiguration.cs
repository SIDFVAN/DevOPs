using Blanche.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blanche.Server.Persistence.Configurations.Customers
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.OwnsOne(x => x.Email).Property(x => x.Value);
            builder.OwnsOne(x => x.CustomerAddress, address =>
            {
                // Without this mapping EF Core does not save the properties since they're getters only.
                // This can be omitted by making them private set, but then you're lying to the domain model.
                address.Property(x => x.Street).HasMaxLength(255);
                address.Property(x => x.Number).HasMaxLength(10);
                address.Property(x => x.City).HasMaxLength(255);
                address.Property(x => x.Country).HasMaxLength(255);
                address.Property(x => x.PostalCode).HasMaxLength(20);
            });
            builder.Property(x => x.FirstName).HasMaxLength(255);
            builder.Property(x => x.LastName).HasMaxLength(255); 
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
        }
    }

}

