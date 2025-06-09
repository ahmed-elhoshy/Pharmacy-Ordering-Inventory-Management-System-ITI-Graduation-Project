#region MyRegion
using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;
#endregion

namespace E_Commerce.InfrastructureLayer.Data.GenericClass
{
    public class MedicineRepository : GenericRepository<Medicine> , IMedicinRepository
    {
        #region DB Context
        private readonly PharmaDbContext context;
        public MedicineRepository(PharmaDbContext context) : base(context)
        {
            this.context = context;
        }
        #endregion
        public async Task<IReadOnlyList<Medicine>> FilterMedicine(string? desc, string? name , string? sort)
        {
            var query = context.Medicines.AsQueryable();
            if (!string.IsNullOrWhiteSpace(desc))
                query = query.Where(b => b.Description == desc);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(T => T.Name == name);

            query = sort switch
            {
                "PriceAse" => query.OrderBy(x => x.Price),
                "PriceDesc" => query.OrderByDescending(x => x.Price),
                _ => query.OrderBy(x => x.Name)
            };
            return await query.ToListAsync();
        }
        public async Task<IReadOnlyList<Medicine>> SearchMedicinesAsync(string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await context.Medicines.ToListAsync();

            searchTerm = searchTerm.ToLower();

            return await context.Medicines
                .Where(p =>
                p.Name.ToLower().StartsWith(searchTerm) ||       
                p.Description.ToLower().StartsWith(searchTerm)).OrderBy(p => p.Name).ToListAsync();
        }

    }
}
