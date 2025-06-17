using AutoMapper;
using E_Commerce.DomainLayer.Interfaces;
using E_Commerce.InfrastructureLayer.Data.DBContext.Repositories;
using PharmacySystem.ApplicationLayer.DTOs.Area;
using PharmacySystem.ApplicationLayer.DTOs.Governate;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.ApplicationLayer.Services;

public class GovernateService : IGovernateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GovernateService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<AreaLookupDto>> GetAreasByGovernateIdAsync(int governateId)
    {
        var areas = await _unitOfWork.AreaRepository.GetAreasByGovernateIdAsync(governateId);
        return _mapper.Map<List<AreaLookupDto>>(areas);
    }

    public async Task<List<GovernateLookupDto>> GetGovernatesAsync()
    {
        var governates = await _unitOfWork.GovernateRepository.GetAllAsync();
        return _mapper.Map<List<GovernateLookupDto>>(governates);
    }


}
