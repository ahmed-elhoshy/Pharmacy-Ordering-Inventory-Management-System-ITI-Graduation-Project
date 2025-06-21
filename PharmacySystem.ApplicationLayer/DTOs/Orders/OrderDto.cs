using PharmacySystem.ApplicationLayer.DTOs.OrderDetails;
using PharmacySystem.DomainLayer.Entities.Constants;

namespace PharmacySystem.ApplicationLayer.DTOs.Orders
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string OrderState { get; set; }
        public string PharmacyName { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string WarehouseName { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
