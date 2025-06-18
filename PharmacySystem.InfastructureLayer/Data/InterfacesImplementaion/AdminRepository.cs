using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;

namespace PharmacySystem.InfastructureLayer.Data.InterfacesImplementaion
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        private readonly PharmaDbContext _context;

        public AdminRepository(PharmaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Admins.AnyAsync(a => a.Email == email);
        }

        public async Task<Admin?> FindByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
} 