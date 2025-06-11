using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines;
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

            //var dtoItems = result.Items.Select(wm => new WarehouseMedicineDto
            //{
            //    MedicineId = wm.MedicineId,
            //    MedicineName = wm.Medicine.Name,
            //    Quantity = wm.Quantity,
            //    Discount = wm.Discount
            //}).ToList();
            var dtoItems = _mapper.Map<List<WarehouseMedicineDto>>(result.Items);


            return new PaginatedResult<WarehouseMedicineDto>
            {
                TotalCount = result.TotalCount,
                Page = result.Page,
                PageSize = result.PageSize,
                Items = dtoItems
            };
        }
        public async Task<IEnumerable<ReadWareHouseDTO>> GetWarehousesByUserAreaAsync(int areaId)
        {
            var warehouses = await warehouseRepository.GetWarehousesByAreaAsync(areaId);
            return _mapper.Map<IEnumerable<ReadWareHouseDTO>>(warehouses);
        }
        public async Task<IEnumerable<ReadWarehouseDetailsDTO>> GetAllAsync()
        {
            var warehouses = await warehouseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadWarehouseDetailsDTO>>(warehouses);
        }

        public async Task<ReadWarehouseDetailsDTO?> GetByIdAsync(int id)
        {
            var warehouse = await warehouseRepository.GetByIdAsync(id);
            return warehouse is null ? null : _mapper.Map<ReadWarehouseDetailsDTO>(warehouse);
        }
        public async Task AddAsync(CreateWarhouseDTO dto)
        {
            var entity = _mapper.Map<WareHouse>(dto);
            await warehouseRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateWareHouseDTO dto)
        {
            var entity = _mapper.Map<WareHouse>(dto);
            await warehouseRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await warehouseRepository.DeleteAsync(id);
        }
    }
}
