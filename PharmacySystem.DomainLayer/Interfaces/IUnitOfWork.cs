using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;

namespace E_Commerce.DomainLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IMedicinRepository medicineRepository { get; set; }
        public IRepresentativeRepository representativeRepository { get; set; }
        public IPharmacyRepository PharmacyRepository { get; set; }
        public IAdminRepository AdminRepository { get; set; }
        public IGenericRepository<Area> AreaRepository { get; }
        Task<bool> SaveAsync();
    }
}
