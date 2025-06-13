using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.ApplicationLayer.Pagination;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;
using PharmacySystem.InfastructureLayer.Data.DBContext;

namespace PharmacySystem.InfastructureLayer.Data.InterfacesImplementaion
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private PharmaDbContext _dbContext;
        public WarehouseRepository(PharmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<WareHouse>> GetAllAsync()
        {
            return await _dbContext.WareHouses.Include(w => w.WareHouseAreas).ThenInclude(w => w.Area).ToListAsync();
        }
        
        public async Task<WareHouse?> GetByIdAsync(int id)
        {
            return await _dbContext.WareHouses
               .Include(w => w.WareHouseAreas).ThenInclude(wa => wa.Area)
               .FirstOrDefaultAsync(w => w.Id == id);
        } public async Task<WareHouse?> GetWarehouseByIdDetailsAsync(int id)
        {
            return await _dbContext.WareHouses
                .Include(w => w.User)
                .Include(w => w.ApprovedByAdmin)
                .Include(w => w.WareHouseAreas).ThenInclude(wa => wa.Area)
                .Include(w => w.WareHouseMedicines).ThenInclude(wm => wm.Medicine)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<IEnumerable<WareHouse>> GetWarehousesByAreaAsync(int areaId)
        {
            return await _dbContext.WareHouses
                .Include(w => w.WareHouseAreas)
                .ThenInclude(wa => wa.Area)
                .Where(w => w.WareHouseAreas.Any(wa => wa.AreaId == areaId))
                 .ToListAsync();
        }
        public async Task<PaginatedResult<WareHouseMedicien>> GetWarehouseMedicinesAsync(int warehouseId, int page, int pageSize)
        {
            var query = _dbContext.WareHouseMediciens
             .Where(wm => wm.WareHouseId == warehouseId)
             .Include(wm => wm.Medicine)
             .AsQueryable();

          

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(); 

            return new PaginatedResult<WareHouseMedicien>
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Items = items
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
            existing.UserId = updated.UserId;
            existing.ApprovedByAdminId = updated.ApprovedByAdminId;

            // Remove and replace areas
            _dbContext.WareHouseAreas.RemoveRange(existing.WareHouseAreas);
            existing.WareHouseAreas = updated.WareHouseAreas;

            // Remove and replace medicines
            _dbContext.WareHouseMediciens.RemoveRange(existing.WareHouseMedicines);
            existing.WareHouseMedicines = updated.WareHouseMedicines;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var warehouse = await _dbContext.WareHouses.FindAsync(id);
            if (warehouse != null)
            {
                _dbContext.WareHouses.Remove(warehouse);
                await _dbContext.SaveChangesAsync();
            }
        }

        

       
    }
}

