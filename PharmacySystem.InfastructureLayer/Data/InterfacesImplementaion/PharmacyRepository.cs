using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;

namespace PharmacySystem.InfastructureLayer.Data.InterfacesImplementaion;

public class PharmacyRepository : GenericRepository<Pharmacy>, IPharmacyRepository
{
    private readonly PharmaDbContext _context;

    public PharmacyRepository(PharmaDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Pharmacies.AnyAsync(p => p.Email == email);
    }
    public async Task<Pharmacy?> FindByEmailAsync(string email)
    {
        return await _context.Set<Pharmacy>()
            .FirstOrDefaultAsync(p => p.Email == email);
    }
}