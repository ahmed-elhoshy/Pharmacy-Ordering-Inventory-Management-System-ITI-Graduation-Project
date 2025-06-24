using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.InfastructureLayer.Data.Config;

namespace PharmacySystem.InfastructureLayer.Data.DBContext
{
    public class PharmaDbContext : DbContext
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
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartWarehouse> CartWarehouses { get; set; }

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

            modelBuilder.Entity<CartWarehouse>()
        .HasOne(cw => cw.Cart)
        .WithMany(c => c.CartWarehouses)
        .HasForeignKey(cw => cw.CartId)
        .OnDelete(DeleteBehavior.Cascade); // عند حذف Cart، احذف CartWarehouses

            // العلاقة بين CartWarehouse و CartItems
            modelBuilder.Entity<CartWarehouse>()
                .HasMany(cw => cw.CartItems)
                .WithOne(ci => ci.CartWarehouse)
                .HasForeignKey(ci => ci.CartWarehouseId)
                .OnDelete(DeleteBehavior.Cascade); // عند حذف CartWarehouse، احذف CartItems

            // العلاقة بين CartItem و Medicine (نمنع الحذف)
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Medicine)
                .WithMany()
                .HasForeignKey(ci => ci.MedicineId)
                .OnDelete(DeleteBehavior.Restrict); // لا تحذف Medicine المرتبط به CartItem

            // تحديد الدقة للحقول العشرية
            modelBuilder.Entity<CartItem>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CartItem>()
                .Property(p => p.Discount)
                .HasPrecision(5, 2);

            modelBuilder.Entity<CartWarehouse>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cart>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2);
        }
    }
}
