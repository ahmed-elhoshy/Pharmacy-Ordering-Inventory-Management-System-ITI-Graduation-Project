using AutoMapper;
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.representative.Create;
using PharmacySystem.ApplicationLayer.DTOs.representative.Read;
using PharmacySystem.ApplicationLayer.DTOs.representative.Update;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmaciesOrdersAndOrderDetails;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.Services
{
    public class RepresentativeService : IRepresentativeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public RepresentativeService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<IEnumerable<GetAllRepresentatitveDto>> GetAllAsync()
        {
            var reps = await _unitOfWork.representativeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetAllRepresentatitveDto>>(reps);
        }

        public async Task<GetRepresentativeByIdDto> GetByIdAsync(int id)
        {
            var rep = await _unitOfWork.representativeRepository.GetByIdAsync(id);
            if (rep == null)    return null;

            var result = _mapper.Map<GetRepresentativeByIdDto>(rep);
            return result;
        }

        public async Task<GetRepresentativeByIdDto> CreateAsync(CreateRepresentativeDto dto)
        {
            var codeExists = await _unitOfWork.representativeRepository.IsCodeExistsAsync(dto.Code);
            if (codeExists)
                throw new Exception("This code already exists");

            var entity = _mapper.Map<Representative>(dto);
            
            // Hash the password before saving
            entity.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            
            await _unitOfWork.representativeRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<GetRepresentativeByIdDto>(entity);
        }

        public async Task<GetRepresentativeByIdDto> UpdateAsync(int id, UpdateRepresentativeDto dto)
        {
            var entity = await _unitOfWork.representativeRepository.GetByIdAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            await _unitOfWork.representativeRepository.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<GetRepresentativeByIdDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.representativeRepository.GetByIdAsync(id);
            if (entity == null) return false;

            await _unitOfWork.representativeRepository.DeleteAsync(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<GetRepresentatitvePharmaciesCountDto> GetPharmaciesCountById(int id)
        {
            var rep = _unitOfWork.representativeRepository.GetCountOfPharmaciesWithRepresentativeId(id)
                .FirstOrDefault(x => x.Id == id);

            return _mapper.Map<GetRepresentatitvePharmaciesCountDto>(rep);
        }

        public async Task<GetRepresentatitvePharmaciesCountDto> GetPharmaciesCountByCode(string code)
        {
            var rep = _unitOfWork.representativeRepository.GetCountOfPharmaciesWithRepresentativeCode(code)
                .FirstOrDefault(x => x.Code == code);

            return _mapper.Map<GetRepresentatitvePharmaciesCountDto>(rep);
        }

        public async Task<GetOrdersPharmaciesCountDto> GetOrdersCountById(int id)
        {
            var rep = _unitOfWork.representativeRepository.GetCountOfOrders(id)
                .FirstOrDefault(x => x.Id == id);

            return _mapper.Map<GetOrdersPharmaciesCountDto>(rep);
        }

        public async Task<RepresentativeLoginResponseDTO> LoginAsync(RepresentativeLoginDTO dto)
        {
            // Retrieve the pharmacy entity by email
            var representative = await _unitOfWork.representativeRepository.FindByEmailAsync(dto.Email);

            if (representative == null)
                return new RepresentativeLoginResponseDTO
                {
                    Success = false,
                    Message = "Invalid email or password."
                };

            // Verify the password against the stored hash
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, representative.Password))
                return new RepresentativeLoginResponseDTO
                {
                    Success = false,
                    Message = "Invalid email or password."
                };

            // Generate JWT token
            var token = GenerateJwtToken(representative);

            return new RepresentativeLoginResponseDTO
            {
                Success = true,
                Message = "Login successful.",
                Token = token
            };
        }

        private string GenerateJwtToken(Representative representative)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, representative.Id.ToString()),
            new Claim(ClaimTypes.Email, representative.Email),
            new Claim(ClaimTypes.Role, "Representative")
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
