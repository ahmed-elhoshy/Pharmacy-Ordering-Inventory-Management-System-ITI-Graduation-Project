using PharmacySystem.ApplicationLayer.DTOs.Warehouse.Login;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Read;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Read;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Update;
using PharmacySystem.ApplicationLayer.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.IServiceInterfaces
{
    public interface IWarehouseService
    {
        Task<PaginatedResult<WarehouseMedicineDto>> GetWarehouseMedicineDtosAsync(int warehouseId, int page, int pageSize);
        Task<bool> WarehouseExistsAsync(int id);
        Task<ReadWareHouseDTO> AddAsync(CreateWarehouseDTO dto);
        Task UpdateAsync(UpdateWareHouseDTO dto);
        Task DeleteAsync(int id);
        Task<List<WareHouseMedicineAreaDto>> GetWarehousesByAreaAndMedicineAsync(int areaId, int medicineId);
        Task<WarehouseLoginResponseDTO> LoginAsync(WarehouseLoginDTO dto);
        Task<PaginatedResult<ReadWareHouseDTO>> GetAllAsync();
        Task<ReadWareHouseDTO> GetByIdAsync(int id);
    }
} 