using PharmacySystem.ApplicationLayer.Common;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Register;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.ApplicationLayer.IServiceInterfaces;

public interface IPharmacyService
{
    Task<ValidationResult?> RegisterPharmacyAsync(PharmacyRegisterDto dto);
    Task<PharmacyLoginResponseDTO> LoginAsync(PharmacyLoginDTO dto);
}