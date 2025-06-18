using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Warehouse.Login;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Read;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Read;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Update;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.ApplicationLayer.Pagination;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Interfaces;

namespace PharmacySystem.ApplicationLayer.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public WarehouseService(IWarehouseRepository warehouseRepository, IMapper mapper, IConfiguration configuration)
        {
            this.warehouseRepository = warehouseRepository;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        public async Task<PaginatedResult<WarehouseMedicineDto>> GetWarehouseMedicineDtosAsync(int warehouseId, int page, int pageSize)
        {
            var result = await warehouseRepository.GetWarehouseMedicinesAsync(warehouseId, page, pageSize);
            var dtoItems = _mapper.Map<List<WarehouseMedicineDto>>(result.Items);
            return new PaginatedResult<WarehouseMedicineDto>
            {
                Items = dtoItems,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<PaginatedResult<SimpleReadWarehouseDTO>> GetWarehousesByUserAreaAsync(int page, int pageSize, int areaId, string? search)
        {
            var result = await warehouseRepository.GetWarehousesByAreaAsync(page, pageSize, areaId, search);
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

        public async Task<PaginatedResult<ReadWareHouseDTO>> GetAllAsync()
        {
            var warehouses = await warehouseRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<ReadWareHouseDTO>>(warehouses.Items);
            return new PaginatedResult<ReadWareHouseDTO>
            {
                Items = dtos.ToList(),
                TotalCount = warehouses.TotalCount,
                PageNumber = warehouses.PageNumber,
                PageSize = warehouses.PageSize
            };
        }

        public async Task<ReadWareHouseDTO> GetByIdAsync(int id)
        {
            var warehouse = await warehouseRepository.GetByIdAsync(id);
            return _mapper.Map<ReadWareHouseDTO>(warehouse);
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

        public async Task<ReadWareHouseDTO> AddAsync(CreateWarehouseDTO dto)
        {
            var warehouse = _mapper.Map<WareHouse>(dto);
            // Hash the password before saving
            warehouse.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            await warehouseRepository.AddAsync(warehouse);
            return _mapper.Map<ReadWareHouseDTO>(warehouse);
        }

        public async Task UpdateAsync(UpdateWareHouseDTO dto)
        {
            var updatedWarehouse = _mapper.Map<WareHouse>(dto);
            await warehouseRepository.UpdateAsync(updatedWarehouse);
        }

        public async Task DeleteAsync(int id)
        {
            await warehouseRepository.DeleteAsync(id);
        }

        public async Task<List<WareHouseMedicineAreaDto>> GetWarehousesByAreaAndMedicineAsync(int areaId, int medicineId)
        {
            var warehouses = await warehouseRepository.GetWarehousesByAreaAndMedicineAsync(areaId, medicineId);

            return warehouses.Select(w =>
            {
                var medicine = w.WareHouseMedicines.FirstOrDefault(wm => wm.MedicineId == medicineId);

                return new WareHouseMedicineAreaDto
                {
                    WarehouseId = w.Id,
                    WareHouseAreaName = w.Address,
                    MedicineId = medicineId,
                    WarehHouseName = w.Name,
                    MedicineName = medicine?.Medicine.Name,
                    Quantity = medicine?.Quantity ?? 0,
                    MedicinePrice = medicine?.Medicine.Price ?? 0,
                    Discount = medicine?.Discount ?? 0,
                    FinalPrice = (medicine?.Medicine.Price ?? 0) * (1 - (medicine?.Discount ?? 0) / 100)
                };
            }).ToList();
        }

        public async Task<WarehouseLoginResponseDTO> LoginAsync(WarehouseLoginDTO dto)
        {
            var warehouse = await warehouseRepository.FindByEmailAsync(dto.Email);

            if (warehouse == null)
                return new WarehouseLoginResponseDTO
                {
                    Success = false,
                    Message = "Invalid email or password."
                };

            // Verify the password against the stored hash
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, warehouse.Password))
                return new WarehouseLoginResponseDTO
                {
                    Success = false,
                    Message = "Invalid email or password."
                };

            // Generate JWT token
            var token = GenerateJwtToken(warehouse);

            return new WarehouseLoginResponseDTO
            {
                Success = true,
                Message = "Login successful.",
                Token = token
            };
        }

        private string GenerateJwtToken(WareHouse warehouse)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, warehouse.Id.ToString()),
                new Claim(ClaimTypes.Email, warehouse.Email),
                new Claim(ClaimTypes.Role, "WareHouse")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
