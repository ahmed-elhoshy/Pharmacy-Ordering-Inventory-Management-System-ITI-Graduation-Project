using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.InfastructureLayer.Data.Config
{
    public class WareHouseConfiguration : IEntityTypeConfiguration<WareHouse>
    {
        public void Configure(EntityTypeBuilder<WareHouse> builder)
        {

            builder.Property(e => e.Address).IsRequired().HasMaxLength(100);

            builder.Property(e => e.Governate).IsRequired().HasMaxLength(50);

            builder.Property(e => e.IsTrusted).IsRequired().HasDefaultValue(false);


            builder.HasOne(e => e.User).WithOne(g => g.WareHouse).HasForeignKey<WareHouse>(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(e => e.User).WithOne(g => g.WareHouse).HasForeignKey<WareHouse>(e => e.ApprovedByAdminId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 