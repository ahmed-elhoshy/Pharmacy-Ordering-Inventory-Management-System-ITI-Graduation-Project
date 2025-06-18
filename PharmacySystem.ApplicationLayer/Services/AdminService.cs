using AutoMapper;
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using PharmacySystem.ApplicationLayer.DTOs.Admin;
using PharmacySystem.ApplicationLayer.DTOs.representative.Create;
using PharmacySystem.ApplicationLayer.DTOs.representative.Read;
using PharmacySystem.ApplicationLayer.DTOs.representative.Update;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Read;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Update;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IRepresentativeService _representativeService;
        private readonly WarehouseService _warehouseService;

        public AdminService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration,
            IRepresentativeService representativeService,
            WarehouseService warehouseService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _representativeService = representativeService;
            _warehouseService = warehouseService;
        }

        #region Representative Operations
        public async Task<GetRepresentativeByIdDto> CreateRepresentativeAsync(CreateRepresentativeDto dto)
        {
            return await _representativeService.CreateAsync(dto);
        }

        public async Task<GetRepresentativeByIdDto> UpdateRepresentativeAsync(int id, UpdateRepresentativeDto dto)
        {
            return await _representativeService.UpdateAsync(id, dto);
        }

        public async Task<bool> DeleteRepresentativeAsync(int id)
        {
            return await _representativeService.DeleteAsync(id);
        }

        public async Task<IEnumerable<GetAllRepresentatitveDto>> GetAllRepresentativesAsync()
        {
            return await _representativeService.GetAllAsync();
        }

        public async Task<GetRepresentativeByIdDto> GetRepresentativeByIdAsync(int id)
        {
            return await _representativeService.GetByIdAsync(id);
        }
        #endregion

        #region Warehouse Operations
        public async Task<ReadWareHouseDTO> CreateWarehouseAsync(CreateWarehouseDTO dto)
        {
            return await _warehouseService.AddAsync(dto);
        }

        public async Task<ReadWareHouseDTO> UpdateWarehouseAsync(int id, UpdateWareHouseDTO dto)
        {
            await _warehouseService.UpdateAsync(dto);
            return await _warehouseService.GetByIdAsync(id);
        }

        public async Task<bool> DeleteWarehouseAsync(int id)
        {
            try
            {
                await _warehouseService.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<ReadWareHouseDTO>> GetAllWarehousesAsync()
        {
            var result = await _warehouseService.GetAllAsync();
            return result.Items;
        }

        public async Task<ReadWareHouseDTO> GetWarehouseByIdAsync(int id)
        {
            return await _warehouseService.GetByIdAsync(id);
        }
        #endregion
    }
} 