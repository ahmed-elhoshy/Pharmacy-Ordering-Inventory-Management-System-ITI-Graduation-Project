using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Login;

namespace PharmacySystem.ApplicationLayer.DTOs.Warehouse.Login
{
    public class WarehouseLoginResponseDTO
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public WarehouseInfoDto Warehouse { get; set; }

    }
} 