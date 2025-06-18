using PharmacySystem.ApplicationLayer.DTOs.Admin;
using PharmacySystem.ApplicationLayer.DTOs.representative.Create;
using PharmacySystem.ApplicationLayer.DTOs.representative.Read;
using PharmacySystem.ApplicationLayer.DTOs.representative.Update;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Read;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Update;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.IServiceInterfaces
{
    public interface IAdminService
    {
        #region Representative Operations
        Task<GetRepresentativeByIdDto> CreateRepresentativeAsync(CreateRepresentativeDto dto);
        Task<GetRepresentativeByIdDto> UpdateRepresentativeAsync(int id, UpdateRepresentativeDto dto);
        Task<bool> DeleteRepresentativeAsync(int id);
        Task<IEnumerable<GetAllRepresentatitveDto>> GetAllRepresentativesAsync();
        Task<GetRepresentativeByIdDto> GetRepresentativeByIdAsync(int id);
        #endregion

        #region Warehouse Operations
        Task<ReadWareHouseDTO> CreateWarehouseAsync(CreateWarehouseDTO dto);
        Task<ReadWareHouseDTO> UpdateWarehouseAsync(int id, UpdateWareHouseDTO dto);
        Task<bool> DeleteWarehouseAsync(int id);
        Task<IEnumerable<ReadWareHouseDTO>> GetAllWarehousesAsync();
        Task<ReadWareHouseDTO> GetWarehouseByIdAsync(int id);
        #endregion
    }
} 