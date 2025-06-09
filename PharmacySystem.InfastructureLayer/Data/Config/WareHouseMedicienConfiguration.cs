using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.InfastructureLayer.Data.Config
{
    public class WareHouseMedicienConfiguration : IEntityTypeConfiguration<WareHouseMedicien>
    {
        public void Configure(EntityTypeBuilder<WareHouseMedicien> builder)
        {
            builder.HasKey(e => new { e.MedicineId, e.WareHouseId });

            builder.Property(e => e.Quantity).IsRequired();

            builder.Property(e => e.Discount).IsRequired().HasColumnType("decimal(5,2)");

            builder.HasOne(e => e.Medicine).WithMany(m => m.WareHouseMedicines).HasForeignKey(e => e.MedicineId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.WareHouse).WithMany(wh => wh.WareHouseMedicines).HasForeignKey(e => e.WareHouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 