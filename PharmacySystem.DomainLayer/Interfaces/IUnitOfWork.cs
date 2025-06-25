using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;

namespace E_Commerce.DomainLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IMedicinRepository medicineRepository { get; set; }
        public ICartWarehouseRepository cartWarehouseRepository { get; set; }
        public ICartItemRepository cartItemRepository { get; set; }
        public ICartWarehousesRepository cartWarehousesRepository { get; set; }
        public IWarehouseMedicineRepository warehouseMedicineRepository { get; set; }
        public IOrderRepository orderRepository { get; set; }
        public IRepresentativeRepository representativeRepository { get; set; }
        public IPharmacyRepository PharmacyRepository { get; set; }
        public IAdminRepository AdminRepository { get; set; }
        //public IGenericRepository<Area> AreaRepository { get; }
        public ICartRepository cartRepository { get; }

        IAreaRepository AreaRepository { get; }
        IGenericRepository<Governate> GovernateRepository { get; }
        IGenericRepository<WareHouseArea> WareHouseAreaRepository { get; }
        Task<bool> SaveAsync();
    }
}
