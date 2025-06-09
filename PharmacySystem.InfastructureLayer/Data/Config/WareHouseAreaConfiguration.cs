using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacySystem.DomainLayer.Entities;


namespace PharmacySystem.InfastructureLayer.Data.Config
{
    internal class WareHouseAreaConfiguration : IEntityTypeConfiguration<WareHouseArea>
    {
        public void Configure(EntityTypeBuilder<WareHouseArea> builder)
        {
            builder.HasKey(e => new { e.AreaId, e.WareHouseId });
            builder.Property(e => e.MinmumPrice).HasColumnType("decimal(18,2)");

            // Configure relationships
            builder.HasOne(e => e.WareHouse).WithMany(wh => wh.WareHouseAreas).HasForeignKey(e => e.WareHouseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Area).WithMany(a => a.WareHouseAreas).HasForeignKey(e => e.AreaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
