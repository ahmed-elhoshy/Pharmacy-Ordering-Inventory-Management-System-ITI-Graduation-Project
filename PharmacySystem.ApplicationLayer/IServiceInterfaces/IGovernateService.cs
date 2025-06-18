using PharmacySystem.ApplicationLayer.DTOs.Area;
using PharmacySystem.ApplicationLayer.DTOs.Governate;

namespace PharmacySystem.ApplicationLayer.IServiceInterfaces;

public interface IGovernateService
{
    Task<List<GovernateLookupDto>> GetGovernatesAsync();
    Task<List<AreaLookupDto>> GetAreasByGovernateIdAsync(int governateId);
}
