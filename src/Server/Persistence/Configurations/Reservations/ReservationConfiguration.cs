using Blanche.Domain.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blanche.Server.Persistence.Configurations.Reservations
{
	public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
		public void Configure(EntityTypeBuilder<Reservation> builder)
		{
			builder.HasOne(r => r.Customer);
			builder.HasOne(r => r.Formula);
			builder.HasMany(r => r.Lines);
		}
	}
}

