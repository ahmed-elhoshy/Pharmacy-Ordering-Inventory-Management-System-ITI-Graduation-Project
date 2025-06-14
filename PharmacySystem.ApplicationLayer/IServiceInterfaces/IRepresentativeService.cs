using PharmacySystem.ApplicationLayer.DTOs.representatitve.Create;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Read;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Update;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmaciesOrdersAndOrderDetails;


namespace PharmacySystem.ApplicationLayer.IServiceInterfaces
{
    public interface IRepresentativeService
    {
        Task<IEnumerable<GetAllRepresentatitveDto>> GetAllAsync();
        Task<GetRepresentatitveByIdDto> GetByIdAsync(int id);
        Task<GetRepresentatitveByIdDto> CreateAsync(CreateRepresentatitveDto dto);
        Task<GetRepresentatitveByIdDto> UpdateAsync(int id, UpdateRepresentativeDto dto);
        Task<bool> DeleteAsync(int id);
        Task<GetRepresentatitvePharmaciesCountDto> GetPharmaciesCountById(int id);
        Task<GetRepresentatitvePharmaciesCountDto> GetPharmaciesCountByCode(string code);
        Task<GetOrdersPharmaciesCountDto> GetOrdersCountById(int id);
    }
}
