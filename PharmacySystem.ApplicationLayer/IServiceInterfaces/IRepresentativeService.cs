#region MyRegion
using PharmacySystem.ApplicationLayer.Common;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Register;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmaciesOrdersAndOrderDetails;
using PharmacySystem.ApplicationLayer.DTOs.representative.Create;
using PharmacySystem.ApplicationLayer.DTOs.representative.Read;
using PharmacySystem.ApplicationLayer.DTOs.representative.Update;
using PharmacySystem.ApplicationLayer.DTOs.Representative.Login;
using PharmacySystem.ApplicationLayer.DTOs.RepresentativeOrder;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseOrders;
using PharmacySystem.ApplicationLayer.Pagination;
using PharmacySystem.DomainLayer.Entities.Constants;
#endregion


namespace PharmacySystem.ApplicationLayer.IServiceInterfaces
{
    public interface IRepresentativeService
    {
        Task<IEnumerable<GetAllRepresentatitveDto>> GetAllAsync();
        Task<GetRepresentativeByIdDto> GetByIdAsync(int id);
        Task<GetRepresentativeByIdDto> CreateAsync(CreateRepresentativeDto dto);
        Task<GetRepresentativeByIdDto> UpdateAsync(int id, UpdateRepresentativeDto dto);
        Task<bool> DeleteAsync(int id);
        Task<GetRepresentatitvePharmaciesCountDto> GetPharmaciesCountById(int id);
        Task<GetOrdersPharmaciesCountDto> GetOrdersCountById(int id);
        Task<RepresentativeLoginResponseDTO> LoginAsync(RepresentativeLoginDTO dto);
        Task<ValidationResult?> ForgotPasswordAsync(PharmacySystem.ApplicationLayer.DTOs.Representative.Login.ForgotPasswordRequestDto dto);
        Task<ValidationResult?> ResetPasswordAsync(PharmacySystem.ApplicationLayer.DTOs.Representative.Login.ResetPasswordRequestDto dto);
        public Task<PaginatedResult<WarehouseOrdersDto>> GetAllOrdersPaginatedByRepresentativeIdAsync(int representativeId,OrderStatus status,int pageNumber,int pageSize);
        public Task<RepresentativeOrderStatsDto> GetOrdersStatsAsync(int representativeId);
    }
}
