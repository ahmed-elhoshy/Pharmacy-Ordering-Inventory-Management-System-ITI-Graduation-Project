

namespace PharmacySystem.ApplicationLayer.DTOs.OrderDetails
{
    public class OrderDetailDto
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
