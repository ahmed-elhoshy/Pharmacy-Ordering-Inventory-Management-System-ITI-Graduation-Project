﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.ApplicationLayer.Pagination;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.DomainLayer.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<PaginatedResult<WareHouse>> GetWarehousesByAreaAsync( int page, int pageSize ,int areaId, string? search);
        Task<PaginatedResult<WareHouse>> GetAllAsync(int page, int pageSize);
        Task<WareHouse?> GetByIdAsync(int id);
        Task<WareHouse?> GetWarehouseByIdDetailsAsync(int id);
        Task<PaginatedResult<WareHouseMedicien>> GetWarehouseMedicinesAsync(
        int warehouseId, int page, int pageSize);
        Task AddAsync(WareHouse warehouse);
        Task UpdateAsync(WareHouse warehouse);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
