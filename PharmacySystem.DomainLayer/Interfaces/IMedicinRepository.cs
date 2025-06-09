using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.DomainLayer.Interfaces
{
    public interface IMedicinRepository : IGenericRepository<Medicine>
    {
        public Task<IReadOnlyList<Medicine>> SearchMedicinesAsync(string? searchTerm);
        public Task<IReadOnlyList<Medicine>> FilterMedicine(string? desc, string? name, string? sort);

    }
}
