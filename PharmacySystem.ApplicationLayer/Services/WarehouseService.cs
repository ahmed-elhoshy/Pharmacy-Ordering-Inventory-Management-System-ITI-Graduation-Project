using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Read;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Read;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Update;
using PharmacySystem.ApplicationLayer.Pagination;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;

namespace PharmacySystem.ApplicationLayer.Services
{
    public class WarehouseService
    {
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IMapper _mapper;
        public WarehouseService(IWarehouseRepository warehouseRepository , IMapper mapper)
        {
            this.warehouseRepository = warehouseRepository;
            this._mapper = mapper;
        }

        public async Task<PaginatedResult<WarehouseMedicineDto>> GetWarehouseMedicineDtosAsync(int warehouseId, int page, int pageSize)
        {
            var result = await warehouseRepository.GetWarehouseMedicinesAsync( warehouseId,page, pageSize);

            var dtoItems = _mapper.Map<List<WarehouseMedicineDto>>(result.Items);


            return new PaginatedResult<WarehouseMedicineDto>
            {
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                Items = dtoItems
            };
        }
        public async Task<PaginatedResult<SimpleReadWarehouseDTO>> GetWarehousesByUserAreaAsync(int page, int pageSize, int areaId, string? search)
        {
            var result = await warehouseRepository.GetWarehousesByAreaAsync( page,  pageSize,  areaId, search);
            var dtoItems = _mapper.Map<IEnumerable<SimpleReadWarehouseDTO>>(result.Items,
             opt => opt.Items["areaId"] = areaId);

            return new PaginatedResult<SimpleReadWarehouseDTO>
            {
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                Items = dtoItems
            };
        }
        public async Task<PaginatedResult<ReadWareHouseDTO>> GetAllAsync(int page, int pageSize)
        {
            var result = await warehouseRepository.GetAllAsync(page, pageSize);
                
            var dtoitems = _mapper.Map<IEnumerable<ReadWareHouseDTO>>(result.Items);

            return new PaginatedResult<ReadWareHouseDTO>
            {
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                Items = dtoitems
            };
        }

        public async Task<ReadWareHouseDTO?> GetByIdAsync(int id)
        {
            var warehouse = await warehouseRepository.GetByIdAsync(id);
            return warehouse is null ? null : _mapper.Map<ReadWareHouseDTO>(warehouse);
        }
        public async Task<ReadWarehouseDetailsDTO?> GetWarehouseByIdDetailsAsync(int id)
        {
            var warehouse = await warehouseRepository.GetWarehouseByIdDetailsAsync(id);
            return warehouse is null ? null : _mapper.Map<ReadWarehouseDetailsDTO>(warehouse);
        }
        public async Task<bool> WarehouseExistsAsync(int id)
        {
            return await warehouseRepository.ExistsAsync(id);
        }

        public async Task<ReadWareHouseDTO> AddAsync(CreateWarhouseDTO dto)
        {
            var warehouse = _mapper.Map<WareHouse>(dto);
            await warehouseRepository.AddAsync(warehouse);
            return _mapper.Map<ReadWareHouseDTO>(warehouse);
        }

        public async Task UpdateAsync(UpdateWareHouseDTO dto)
        {
           // Map DTO to a WareHouse entity but don't attach to DB
            var updatedWarehouse = _mapper.Map<WareHouse>(dto);
           await warehouseRepository.UpdateAsync(updatedWarehouse);
        }

        public async Task DeleteAsync(int id)
        {
            await warehouseRepository.DeleteAsync(id);
        }
    }
}
