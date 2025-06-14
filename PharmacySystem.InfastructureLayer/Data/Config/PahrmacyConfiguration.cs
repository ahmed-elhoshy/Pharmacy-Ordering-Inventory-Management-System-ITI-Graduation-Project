using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmacySystem.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.InfastructureLayer.Data.Config
{
    public class PahrmacyConfiguration : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {


            builder.Property(e => e.Address).IsRequired().HasMaxLength(100);

            builder.Property(e => e.Governate).IsRequired().HasMaxLength(50);

            builder.HasOne(e => e.Area).WithMany(a => a.Pharmacies).HasForeignKey(e => e.AreaId)
                   .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(e => e.ApprovedByRepresentative).WithMany(r => r.pharmacies).HasForeignKey(e => e.ApprovedByRepresentativeId)
            //       .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
