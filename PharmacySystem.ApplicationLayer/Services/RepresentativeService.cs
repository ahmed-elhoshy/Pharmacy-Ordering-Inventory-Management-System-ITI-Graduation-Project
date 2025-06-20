using AutoMapper;
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PharmacySystem.ApplicationLayer.DTOs.OrderDetails;
using PharmacySystem.ApplicationLayer.DTOs.Orders;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmaciesOrdersAndOrderDetails;
using PharmacySystem.ApplicationLayer.DTOs.representative.Create;
using PharmacySystem.ApplicationLayer.DTOs.representative.Read;
using PharmacySystem.ApplicationLayer.DTOs.representative.Update;
using PharmacySystem.ApplicationLayer.DTOs.Representative.Login;
using PharmacySystem.ApplicationLayer.DTOs.RepresentativeOrder;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseOrders;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Entities.Constants;
using PharmacySystem.DomainLayer.Interfaces;
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

        private string GenerateRepresentativeCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            // Generate 5 random characters and prepend 'R'
            return "R" + new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<IEnumerable<GetAllRepresentatitveDto>> GetAllAsync()
        {
            var reps = await _unitOfWork.representativeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetAllRepresentatitveDto>>(reps);
        }

        public async Task<GetRepresentativeByIdDto> GetByIdAsync(int id)
        {
            var rep = await _unitOfWork.representativeRepository.GetByIdAsync(id);
            if (rep == null) return null;

            var result = _mapper.Map<GetRepresentativeByIdDto>(rep);
            return result;
        }

        public async Task<GetRepresentativeByIdDto> CreateAsync(CreateRepresentativeDto dto)
        {
            // Generate a unique code
            string generatedCode;
            do
            {
                generatedCode = GenerateRepresentativeCode();
            } while (await _unitOfWork.representativeRepository.IsCodeExistsAsync(generatedCode));

            var entity = _mapper.Map<Representative>(dto);
            entity.Code = generatedCode;

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
                Token = token,
                Representative = _mapper.Map<RepresentstiveInfoDto>(representative)

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

        #region Get Orders Stats Async
        public async Task<RepresentativeOrderStatsDto> GetOrdersStatsAsync(int representativeId)
        {
            var orders = await _unitOfWork.orderRepository.GetOrdersByRepresentativeIdIncludingPharmicesAsync(representativeId);

            int pharmacyCount = orders.Select(o => o.PharmacyId).Distinct().Count();

            decimal revenue = orders.Where(o => o.Status == OrderStatus.Delivered)
                .Sum(o => o.TotalPrice);

            var stats = Enum.GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .ToDictionary(
                    status => status.ToString(),
                    status => orders.Count(o => o.Status == status)
                );

            return new RepresentativeOrderStatsDto
            {
                PharmacyCount = pharmacyCount,
                Revenue = revenue,
                Stats = stats
            };
        }


        #endregion

        #region Get All WareHouse Including List Of Order For Pharmacies
        public async Task<IEnumerable<WarehouseOrdersDto>> GetRepresentativeWarehouseOrdersAsync(int representativeId)
        {
            var orders = await _unitOfWork.orderRepository.GetAllOrdersByRepresentativeIdAsync(representativeId);

            var grouped = orders
                .GroupBy(o => o.WareHouse.Name)
                .Select(g => new WarehouseOrdersDto
                {
                    WarehouseName = g.Key,
                    OrdersCount = g.Count(),
                    DeliveredRevenue = g
                    .Where(o => o.Status == OrderStatus.Delivered)
                    .Sum(o => o.TotalPrice),
                    TotalPrice = g.Sum(o => o.TotalPrice),
                    Orders = g.Select(o => new OrderDto
                    {
                        OrderId = o.Id,
                        OrderState = o.Status.ToString(),
                        PharmacyName = o.Pharmacy.Name,
                        WarehouseName = o.WareHouse.Name,
                        OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
                        {
                            MedicineId = od.MedicineId,
                            MedicineName = od.Medicine.Name,
                            Quantity = od.Quntity,
                            Price = od.Price
                        }).ToList()
                    }).ToList()
                }).ToList();

            return grouped;
        }
        #endregion
    }
}
