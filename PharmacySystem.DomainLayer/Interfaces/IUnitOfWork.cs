using PharmacySystem.DomainLayer.Interfaces;

namespace E_Commerce.DomainLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IMedicinRepository medicinRepository { get; set; }
        public IRepresentitiveRepository representitiveRepository { get; set; }
        Task<bool> SaveAsync();

    }
}
