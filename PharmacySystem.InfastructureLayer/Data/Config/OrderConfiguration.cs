using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.InfastructureLayer.Data.Config
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            // Property configurations
            builder.Property(o => o.Quntity).IsRequired(); // Fixed typo
            builder.Property(o => o.TotalPrice).IsRequired().HasPrecision(18, 2);

            // Relationships
            builder.HasOne(o => o.Pharmacy)
                   .WithMany(p => p.Orders)
                   .HasForeignKey(o => o.PharmacyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.WareHouse)
                   .WithMany(w => w.Orders)
                   .HasForeignKey(o => o.WareHouseId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(o => o.OrderDetails)
                   .WithOne(od => od.Order)
                   .HasForeignKey(od => od.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 