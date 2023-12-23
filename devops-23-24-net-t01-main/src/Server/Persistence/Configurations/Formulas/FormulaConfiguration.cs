using Blanche.Domain.Formulas;
using Blanche.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Blanche.Server.Persistence.Configurations.Formulas
{
	public class FormulaConfiguration : IEntityTypeConfiguration<Formula>
	{
        public void Configure(EntityTypeBuilder<Formula> builder)
        {
            builder.Property(f => f.PricePerDays)
                .HasConversion(
                    c => JsonConvert.SerializeObject(c),
                    c => JsonConvert.DeserializeObject<Dictionary<int, double>>(c));

            //builder.HasKey(f => f.Id);

        //    builder.Property(f => f.Name)
        //        .IsRequired()
        //        .HasMaxLength(255)
        //        .HasColumnName(nameof(Formula.Name));


        //    builder.Property(f => f.Description)
        //        .IsRequired()
        //        .HasMaxLength(255)
        //        .HasColumnName(nameof(Formula.Description));

        //    builder.Property(f => f.Days)
        //        .IsRequired()
        //        .HasColumnName(nameof(Formula.Days));

        //    builder.Property(f => f.Price)
        //        .IsRequired()
        //        .HasColumnName(nameof(Formula.Price));

        //    builder.Property(f => f.ImageUrl)
        //        .HasMaxLength(255)
        //        .HasColumnName(nameof(Formula.ImageUrl));

        }
    }
}

