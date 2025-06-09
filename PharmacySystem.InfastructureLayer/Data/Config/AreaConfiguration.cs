using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.InfastructureLayer.Data.Config
{
    public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(e => e.Governate).WithMany(g => g.Areas).HasForeignKey(e => e.GovernateId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 