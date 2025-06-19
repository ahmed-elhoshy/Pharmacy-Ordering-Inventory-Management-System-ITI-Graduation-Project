using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Entities.Constants;

namespace PharmacySystem.DomainLayer.Interfaces
{
    public interface IRepresentativeRepository : IGenericRepository<Representative> 
    {
        IQueryable<Representative> GetCountOfPharmaciesWithRepresentativeId(int RepresentativeId);
        IQueryable<Representative> GetCountOfOrders(int RepresentativeId);
        Task<bool> IsCodeExistsAsync(string code);
        Task<Representative?> FindByEmailAsync(string email);
    }
}
