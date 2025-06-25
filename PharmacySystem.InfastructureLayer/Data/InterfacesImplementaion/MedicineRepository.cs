#region MyRegion
using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using PharmacySystem.ApplicationLayer.DTOs.Medicines;
using PharmacySystem.ApplicationLayer.Pagination;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Entities.Constants;
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

        public async Task<IReadOnlyList<Medicine>> GetMedicinesByAreaAsync(int areaId)
        {
            var medicines = await context.Medicines
                .AsNoTracking()
          .Include(m => m.WareHouseMedicines)
              .ThenInclude(wm => wm.WareHouse)
                  .ThenInclude(w => w.WareHouseAreas)
          .Where(m => m.WareHouseMedicines
              .Any(wm => wm.WareHouse.WareHouseAreas.Any(wa => wa.AreaId == areaId)))
          .ToListAsync();
            return medicines.DistinctBy(m => m.Id).ToList();
        }
        public async Task<PaginatedResult<Medicine>> GetMedicinesByAreaAsync(int areaId, int page, int pageSize)
        {
            var query = context.Medicines
                .AsNoTracking()
                .Include(m => m.WareHouseMedicines)
                    .ThenInclude(wm => wm.WareHouse)
                        .ThenInclude(w => w.WareHouseAreas)
                .Where(m => m.WareHouseMedicines
                    .Any(wm => wm.WareHouse.WareHouseAreas.Any(wa => wa.AreaId == areaId)))
                .OrderBy(m => m.ArabicName)
                .Distinct();

            var totalCount = await query.CountAsync();

            var pagedMedicines = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Medicine>
            {
                Items = pagedMedicines,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }


        public async Task<PaginatedResult<Medicine>> SearchMedicinesByAreaAndNameAsync(int areaId,  int page, int pageSize, string searchTerm , string type)
        {
            var query = context.Medicines
                .AsNoTracking()
                .Include(m => m.WareHouseMedicines)
                    .ThenInclude(wm => wm.WareHouse)
                        .ThenInclude(w => w.WareHouseAreas)
                .Where(m =>
                    m.WareHouseMedicines
                        .Any(wm => wm.WareHouse.WareHouseAreas.Any(wa => wa.AreaId == areaId))
                );

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(m =>
                    m.ArabicName.Contains(searchTerm) ||
                    m.Name.Contains(searchTerm)
                );
            }

            if (!string.IsNullOrWhiteSpace(type) && Enum.TryParse<MedicineTypes>(type, true, out var parsedType))
            {
                query = query.Where(m => m.Drug == parsedType);
            }


            var totalCount = await query.CountAsync();

            var pagedMedicines = await query
                .OrderBy(m => m.ArabicName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Medicine>
            {
                Items = pagedMedicines,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<IReadOnlyList<Medicine>> SearchMedicinesAsync(string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await context.Medicines.ToListAsync();

            searchTerm = searchTerm.ToLower();

            return await context.Medicines
                .Where(p => p.Name.ToLower().StartsWith(searchTerm) ||       
                p.Description.ToLower().StartsWith(searchTerm)).OrderBy(p => p.Name).ToListAsync();
        }

    }

}
