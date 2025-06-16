using AutoMapper;
using E_Commerce.DomainLayer.Interfaces;
using PharmacySystem.ApplicationLayer.Common;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Register;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.DomainLayer.Entities;


namespace PharmacySystem.ApplicationLayer.Services;

public class PharmacyService(IUnitOfWork unitOfWork,
    IMapper mapper) : IPharmacyService
{


    public async Task<Result> RegisterAsync(PharmacyRegisterDto dto)
    {
        // Check if Pharmacy with the same email already exists
        var emailExists = await unitOfWork.PharmacyRepository.EmailExistsAsync(dto.Email);
        if (emailExists)
            return Result.Fail("Pharmacy with this email already exists.");

        // Check if AreaId exists
        var area = await unitOfWork.AreaRepository.GetByIdAsync(dto.AreaId);
        if (area == null)
            return Result.Fail("Invalid AreaId: Area does not exist.");

        // Find Representative by Code
        var representative = unitOfWork.representitiveRepository
        .FindAsync(r => r.Code == dto.RepresentativeCode).FirstOrDefault();

        if (representative == null)
            return Result.Fail("Invalid Representative Code: Representative does not exist.");

        // Map from DTO to Entity
        var pharmacy = mapper.Map<Pharmacy>(dto);

        // Set the RepresentativeId foreign key
        pharmacy.RepresentativeId = representative.Id;

        // Hash the password before saving
        pharmacy.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        // Add to repository
        await unitOfWork.PharmacyRepository.AddAsync(pharmacy);
        await unitOfWork.SaveAsync();

        return Result.Ok("Pharmacy registered successfully.");
    }

    public async Task<Result> LoginAsync(PharmacyLoginDTO dto)
    {
        // Retrieve the pharmacy entity by email
        var pharmacy = await unitOfWork.PharmacyRepository
            .FindByEmailAsync(dto.Email); // You need a method that returns the Pharmacy entity

        if (pharmacy == null)
            return Result.Fail("Invalid email or password.");

        // Verify the password against the stored hash
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, pharmacy.Password))
            return Result.Fail("Invalid email or password.");

        return Result.Ok("Login successful.");
    }
}
