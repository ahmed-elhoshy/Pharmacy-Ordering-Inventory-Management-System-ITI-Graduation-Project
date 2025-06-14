
using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.DomainLayer.Interfaces
{
    public interface IRepresentitiveRepository : IGenericRepository<Representative> 
    {
        IQueryable<Representative> GetCountOfPharmaciesWithRepresentitiveId(int RepresentativeId);
        IQueryable<Representative> GetCountOfPharmaciesWithRepresentitivecode(string RepresentativeCode);
        IQueryable<Representative> GetCountOfOrders(int RepresentativeId);
        Task<bool> IsCodeExistsAsync(string code);
    }
}
