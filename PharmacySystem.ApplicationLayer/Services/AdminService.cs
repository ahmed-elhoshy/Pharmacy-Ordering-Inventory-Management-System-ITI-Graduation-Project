using AutoMapper;
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PharmacySystem.ApplicationLayer.DTOs.Admin;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        #region Admin Operations
        public async Task<AdminResponseDto> CreateAdminAsync(CreateAdminDto dto)
        {
            // Check if admin with the same email already exists
            var emailExists = await _unitOfWork.AdminRepository.EmailExistsAsync(dto.Email);
            if (emailExists)
                throw new Exception("Admin with this email already exists.");

            var admin = _mapper.Map<Admin>(dto);
            // Hash the password before saving
            admin.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _unitOfWork.AdminRepository.AddAsync(admin);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<AdminResponseDto>(admin);
        }

        public async Task<AdminResponseDto> UpdateAdminAsync(int id, UpdateAdminDto dto)
        {
            var admin = await _unitOfWork.AdminRepository.GetByIdAsync(id);
            if (admin == null)
                throw new Exception("Admin not found.");

            // Check if email is being changed and if it already exists
            if (admin.Email != dto.Email)
            {
                var emailExists = await _unitOfWork.AdminRepository.EmailExistsAsync(dto.Email);
                if (emailExists)
                    throw new Exception("Admin with this email already exists.");
            }

            _mapper.Map(dto, admin);
            await _unitOfWork.AdminRepository.UpdateAsync(admin);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<AdminResponseDto>(admin);
        }

        public async Task<bool> DeleteAdminAsync(int id)
        {
            var admin = await _unitOfWork.AdminRepository.GetByIdAsync(id);
            if (admin == null)
                return false;

            await _unitOfWork.AdminRepository.DeleteAsync(admin);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<AdminResponseDto>> GetAllAdminsAsync()
        {
            var admins = await _unitOfWork.AdminRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AdminResponseDto>>(admins);
        }

        public async Task<AdminResponseDto> GetAdminByIdAsync(int id)
        {
            var admin = await _unitOfWork.AdminRepository.GetByIdAsync(id);
            if (admin == null)
                throw new Exception("Admin not found.");

            return _mapper.Map<AdminResponseDto>(admin);
        }
        #endregion

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

        #region Login Operations
        public async Task<AdminLoginResponseDTO> LoginAsync(AdminLoginDTO dto)
        {
            // Retrieve the pharmacy entity by email
            var admin = await _unitOfWork.AdminRepository.FindByEmailAsync(dto.Email);

            if (admin == null)
                return new AdminLoginResponseDTO
                {
                    Success = false,
                    Message = "Invalid email or password."
                };

            // Verify the password against the stored hash
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, admin.Password))
                return new AdminLoginResponseDTO
                {
                    Success = false,
                    Message = "Invalid email or password."
                };

            // Generate JWT token
            var token = GenerateJwtToken(admin);

            return new AdminLoginResponseDTO
            {
                Success = true,
                Message = "Login successful.",
                Token = token
            };
        }

        private string GenerateJwtToken(Admin admin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
            new Claim(ClaimTypes.Email, admin.Email),
            new Claim(ClaimTypes.Role, "Admin")
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
        #endregion
    }
} 