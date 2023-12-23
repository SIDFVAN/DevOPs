using Blanche.Domain.Formulas;
using Blanche.Domain.Invoices;
using Blanche.Domain.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blanche.Server.Persistence.Configurations.Reservations
{
	public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
		public void Configure(EntityTypeBuilder<Reservation> builder)
		{
            builder.HasMany(r => r.Invoices)
              .WithOne(i => i.Reservation)
              .HasForeignKey(i => i.ReservationId)
              .IsRequired(false);
        }
	}
}
