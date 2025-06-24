

namespace PharmacySystem.ApplicationLayer.DTOs.OrderDetails
{
    public class OrderDetailDto
    {
        public int MedicineId { get; set; }
        public string ArabicMedicineName { get; set; }
        public string EnglishMedicineName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
