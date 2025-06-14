using AutoMapper;
using E_Commerce.DomainLayer.Interfaces;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Create;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Read;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Update;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmaciesOrdersAndOrderDetails;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.Services
{
    public class RepresentativeService : IRepresentativeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RepresentativeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllRepresentatitveDto>> GetAllAsync()
        {
            var reps = await _unitOfWork.representitiveRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetAllRepresentatitveDto>>(reps);
        }

        public async Task<GetRepresentatitveByIdDto> GetByIdAsync(int id)
        {
            var rep = await _unitOfWork.representitiveRepository.GetByIdAsync(id);
            if (rep == null)    return null;

            var result = _mapper.Map<GetRepresentatitveByIdDto>(rep);
            return result;
        }

        public async Task<GetRepresentatitveByIdDto> CreateAsync(CreateRepresentatitveDto dto)
        {
            var codeExists = await _unitOfWork.representitiveRepository.IsCodeExistsAsync(dto.Code);
            if (codeExists)
                throw new Exception("This code already exists");

            var entity = _mapper.Map<Representative>(dto);
            await _unitOfWork.representitiveRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<GetRepresentatitveByIdDto>(entity);
        }

        public async Task<GetRepresentatitveByIdDto> UpdateAsync(int id, UpdateRepresentativeDto dto)
        {
            var entity = await _unitOfWork.representitiveRepository.GetByIdAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            await _unitOfWork.representitiveRepository.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<GetRepresentatitveByIdDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.representitiveRepository.GetByIdAsync(id);
            if (entity == null) return false;

            await _unitOfWork.representitiveRepository.DeleteAsync(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<GetRepresentatitvePharmaciesCountDto> GetPharmaciesCountById(int id)
        {
            var rep = _unitOfWork.representitiveRepository.GetCountOfPharmaciesWithRepresentitiveId(id)
                .FirstOrDefault(x => x.Id == id);

            return _mapper.Map<GetRepresentatitvePharmaciesCountDto>(rep);
        }

        public async Task<GetRepresentatitvePharmaciesCountDto> GetPharmaciesCountByCode(string code)
        {
            var rep = _unitOfWork.representitiveRepository.GetCountOfPharmaciesWithRepresentitivecode(code)
                .FirstOrDefault(x => x.Code == code);

            return _mapper.Map<GetRepresentatitvePharmaciesCountDto>(rep);
        }

        public async Task<GetOrdersPharmaciesCountDto> GetOrdersCountById(int id)
        {
            var rep = _unitOfWork.representitiveRepository.GetCountOfOrders(id)
                .FirstOrDefault(x => x.Id == id);

            return _mapper.Map<GetOrdersPharmaciesCountDto>(rep);
        }
    }
}
