using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.InfastructureLayer.Data.Config
{
    public class RepresentativeConfiguration : IEntityTypeConfiguration<Representative>
    {
        public void Configure(EntityTypeBuilder<Representative> builder)
        {

            builder.Property(e => e.Code).IsRequired();

            builder.Property(e => e.Address).IsRequired().HasMaxLength(100);

            builder.Property(e => e.Governate).IsRequired().HasMaxLength(50);
        }
    }
} 