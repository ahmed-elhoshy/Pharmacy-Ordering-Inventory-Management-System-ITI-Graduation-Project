using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.DomainLayer.Interfaces;

public interface IAreaRepository : IGenericRepository<Area>
{
    Task<List<Area>> GetAreasByGovernateIdAsync(int governateId);
}
