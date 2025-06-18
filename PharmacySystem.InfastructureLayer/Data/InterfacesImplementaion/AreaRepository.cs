using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;

namespace PharmacySystem.InfastructureLayer.Data.InterfacesImplementaion;

public class AreaRepository : GenericRepository<Area>, IAreaRepository
{
    private readonly PharmaDbContext _context;

    public AreaRepository(PharmaDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<Area>> GetAreasByGovernateIdAsync(int governateId)
    {
        return await _context.Areas
            .Where(a => a.GovernateId == governateId)
            .ToListAsync();
    }
}
