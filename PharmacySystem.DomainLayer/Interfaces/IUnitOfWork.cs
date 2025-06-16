using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;

namespace E_Commerce.DomainLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IMedicinRepository medicinRepository { get; set; }
        public IRepresentitiveRepository representitiveRepository { get; set; }
        public IPharmacyRepository PharmacyRepository { get; set; }
        IGenericRepository<Area> AreaRepository { get; }
        Task<bool> SaveAsync();

    }
}
