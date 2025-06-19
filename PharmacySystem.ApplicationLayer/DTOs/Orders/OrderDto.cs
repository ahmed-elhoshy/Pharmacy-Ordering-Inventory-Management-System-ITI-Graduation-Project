using PharmacySystem.ApplicationLayer.DTOs.OrderDetails;
using PharmacySystem.DomainLayer.Entities.Constants;

namespace PharmacySystem.ApplicationLayer.DTOs.Orders
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string OrderState { get; set; }
        public string PharmacyName { get; set; }
        public string WarehouseName { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
