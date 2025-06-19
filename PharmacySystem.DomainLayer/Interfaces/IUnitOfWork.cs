using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;

namespace E_Commerce.DomainLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IMedicinRepository medicineRepository { get; set; }
        public IOrderRepository orderRepository { get; set; }
        public IRepresentativeRepository representativeRepository { get; set; }
        public IPharmacyRepository PharmacyRepository { get; set; }
        public IAdminRepository AdminRepository { get; set; }
        //public IGenericRepository<Area> AreaRepository { get; }
        IAreaRepository AreaRepository { get; }
        IGenericRepository<Governate> GovernateRepository { get; }
        Task<bool> SaveAsync();
    }
}
