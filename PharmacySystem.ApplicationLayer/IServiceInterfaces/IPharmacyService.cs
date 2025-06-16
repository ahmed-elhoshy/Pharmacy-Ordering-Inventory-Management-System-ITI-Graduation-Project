using PharmacySystem.ApplicationLayer.Common;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Register;

namespace PharmacySystem.ApplicationLayer.IServiceInterfaces;

public interface IPharmacyService
{
    Task<Result> RegisterAsync(PharmacyRegisterDto dto);
    Task<Result> LoginAsync(PharmacyLoginDTO dto);
}