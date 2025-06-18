using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.DomainLayer.Interfaces
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        Task<bool> EmailExistsAsync(string email);
        Task<Admin?> FindByEmailAsync(string email);
    }
} 