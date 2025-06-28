using PharmacySystem.ApplicationLayer.DTOs.Orders;
using PharmacySystem.ApplicationLayer.Pagination;
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
        Task<PaginatedResult<OrderMedicineDto>> GetOrdersForPharmacyByStatus(int pharmacyId, int page, int pageSize, OrderStatus? status = null);
        Task<PaginatedResult<OrderMedicineDto>> GetOrdersForPharmacy(int pharmacyId, int page, int pageSize, OrderStatus? status = null);

        Task<List<OrderDetailsDto>> GetOrdersDetails(int orderId);
    }
}
