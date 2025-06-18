using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.InfastructureLayer.Data.Config
{
    public class RepresentativeConfiguration : IEntityTypeConfiguration<Representative>
    {
        public void Configure(EntityTypeBuilder<Representative> builder)
        {
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Governate)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Password)
                .IsRequired();

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
} 