using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.ApplicationLayer.Pagination;
using PharmacySystem.DomainLayer.Entities;
namespace PharmacySystem.DomainLayer.Interfaces
{
    public interface IMedicinRepository : IGenericRepository<Medicine>
    {
        public Task<IReadOnlyList<Medicine>> SearchMedicinesAsync(string? searchTerm);
        public Task<IReadOnlyList<Medicine>> FilterMedicine(string? desc, string? name, string? sort);
        Task<IReadOnlyList<Medicine>> GetMedicinesByAreaAsync(int areaId);
        Task<PaginatedResult<Medicine>> GetMedicinesByAreaAsync(
       int areaId, int page, int pageSize);
        Task<PaginatedResult<Medicine>> SearchMedicinesByAreaAndNameAsync(
      int areaId, int page, int pageSize, string searchTerm);

    }
}
