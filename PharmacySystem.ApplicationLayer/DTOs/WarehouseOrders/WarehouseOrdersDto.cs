using PharmacySystem.ApplicationLayer.DTOs.Orders;

namespace PharmacySystem.ApplicationLayer.DTOs.WarehouseOrders
{
    public class WarehouseOrdersDto
    {
        public string WarehouseName { get; set; }
        public int OrdersCount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DeliveredRevenue { get; set; }

        public List<OrderDto> Orders { get; set; }
    }
}
