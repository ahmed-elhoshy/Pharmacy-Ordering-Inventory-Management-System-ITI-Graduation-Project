using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.ApplicationLayer.Pagination;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;

namespace PharmacySystem.InfastructureLayer.Data.InterfacesImplementaion
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly PharmaDbContext _dbContext;

        public WarehouseRepository(PharmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<WareHouse>> GetAllAsync()
        {
            var query = _dbContext.WareHouses
                .Include(w => w.WareHouseAreas)
                .ThenInclude(w => w.Area)
                .AsQueryable();

            int totalCount = await query.CountAsync();


            return new PaginatedResult<WareHouse>
            {
                TotalCount = totalCount,
                Items = query.ToList()
            };
        }

        public async Task<PaginatedResult<WareHouse>> GetWarehousesByAreaAsync(int page, int pageSize, int areaId, string? search)
        {
            IOrderedQueryable<WareHouse> query = _dbContext.WareHouses
                .Include(w => w.WareHouseAreas)
                .ThenInclude(wa => wa.Area)
                .Where(w => w.WareHouseAreas.Any(wa => wa.AreaId == areaId))
                .OrderBy(w => w.WareHouseAreas
                    .Where(wa => wa.AreaId == areaId)
                    .Select(wa => wa.MinmumPrice)
                    .FirstOrDefault());

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query
                    .Where(w => w.Name != null && w.Name.Contains(search))
                    .OrderBy(w => w.WareHouseAreas
                        .Where(wa => wa.AreaId == areaId)
                        .Select(wa => wa.MinmumPrice)
                        .FirstOrDefault());
            }

            int totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<WareHouse>
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<WareHouse?> GetByIdAsync(int id)
        {
            return await _dbContext.WareHouses
                .Include(w => w.WareHouseAreas)
                .ThenInclude(wa => wa.Area)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<WareHouse?> GetWarehouseByIdDetailsAsync(int id)
        {
            return await _dbContext.WareHouses
                .Include(w => w.WareHouseAreas).ThenInclude(wa => wa.Area)
                .Include(w => w.WareHouseMedicines).ThenInclude(wm => wm.Medicine)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<PaginatedResult<WareHouseMedicien>> GetWarehouseMedicinesAsync(int warehouseId, int page, int pageSize , string ? search)
        {
            var query = _dbContext.WareHouseMediciens
                .Where(wm => wm.WareHouseId == warehouseId)
                .Include(wm => wm.Medicine)
                .AsQueryable();
            if(!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(q => q.Medicine.Name!= null && q.Medicine.Name.Contains(search) || q.Medicine.ArabicName != null && q.Medicine.ArabicName.Contains(search)).OrderBy(m => m.Discount);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<WareHouseMedicien>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task AddAsync(WareHouse warehouse)
        {
            await _dbContext.WareHouses.AddAsync(warehouse);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.WareHouses.AnyAsync(w => w.Id == id);
        }

        public async Task UpdateAsync(WareHouse updated)
        {
            var existing = await _dbContext.WareHouses
                .Include(w => w.WareHouseAreas)
                .Include(w => w.WareHouseMedicines)
                .FirstOrDefaultAsync(w => w.Id == updated.Id);

            if (existing == null)
                throw new Exception("Warehouse not found");

            // Update scalar properties
            existing.Address = updated.Address;
            existing.Governate = updated.Governate;
            existing.IsTrusted = updated.IsTrusted;
            existing.IsWarehouseApproved = updated.IsWarehouseApproved;
            existing.Id = updated.Id;
            existing.ApprovedByAdminId = updated.ApprovedByAdminId;
            existing.Name = updated.Name;
            existing.Email = updated.Email;
            existing.Phone = updated.Phone;
            existing.ImageUrl = updated.ImageUrl;

            // Replace areas
            _dbContext.WareHouseAreas.RemoveRange(existing.WareHouseAreas);
            existing.WareHouseAreas = updated.WareHouseAreas;

            // Replace medicines
            _dbContext.WareHouseMediciens.RemoveRange(existing.WareHouseMedicines);
            existing.WareHouseMedicines = updated.WareHouseMedicines;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var warehouse = await _dbContext.WareHouses
                .Include(w => w.WareHouseMedicines)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (warehouse == null)
                throw new Exception("Warehouse not found");

            if (warehouse.WareHouseMedicines.Any())
                _dbContext.WareHouseMediciens.RemoveRange(warehouse.WareHouseMedicines);

            _dbContext.WareHouses.Remove(warehouse);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<WareHouse>> GetWarehousesByAreaAndMedicineAsync(int areaId, int medicineId)
        {
            return await _dbContext.WareHouses
                .Include(w => w.WareHouseAreas)
                .Include(w => w.WareHouseMedicines)
                    .ThenInclude(wm => wm.Medicine)
                .Where(w =>
                    w.WareHouseAreas.Any(wa => wa.Area.Id == areaId) &&
                    w.WareHouseMedicines.Any(wm => wm.MedicineId == medicineId))
                .ToListAsync();
        }
        public async Task<WareHouse?> FindByEmailAsync(string email)
        {
            return await _dbContext.Set<WareHouse>()
                .FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}
