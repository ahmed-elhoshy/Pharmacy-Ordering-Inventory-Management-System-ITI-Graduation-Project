using PharmacySystem.ApplicationLayer.DTOs.Orders;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Entities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.IServiceInterfaces
{
    public interface IOrderService
    {
        Task<List<OrderToWarehouseDto>> GetOrdersForWarehouseAsync(int warehouseId);
        Task<List<OrderMedicineDto>> GetOrdersForPharmacy(int pharmacyId);
        Task<List<OrderMedicineDto>> GetOrdersForPharmacyByStatus(int pharmacyId,OrderStatus status);
        Task<List<OrderDetailsDto>> GetOrdersDetails(int orderId);
    }
}
