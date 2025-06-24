using PharmacySystem.ApplicationLayer.DTOs.Orders;
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
    }
}
