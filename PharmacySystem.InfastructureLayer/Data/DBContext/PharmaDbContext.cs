using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.InfastructureLayer.Data.Config;

namespace PharmacySystem.InfastructureLayer.Data.DBContext
{
    public class PharmaDbContext : IdentityDbContext<User>
    {
        public PharmaDbContext(DbContextOptions<PharmaDbContext> options) :base(options)
        {
            
        }
        public DbSet<Governate> Governates { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Representative> Representatives { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<WareHouseMedicien> WareHouseMediciens { get; set; }
        public DbSet<WareHouseArea> WareHouseAreas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AreaConfiguration());
            modelBuilder.ApplyConfiguration(new PahrmacyConfiguration());
            modelBuilder.ApplyConfiguration(new RepresentativeConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseMedicienConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseAreaConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }
    }
}
