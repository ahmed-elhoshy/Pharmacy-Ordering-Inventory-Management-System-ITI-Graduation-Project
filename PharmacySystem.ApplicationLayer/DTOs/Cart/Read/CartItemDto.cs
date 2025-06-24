namespace PharmacySystem.ApplicationLayer.DTOs.Cart.Read
{
    public class CartItemDto
    {
        public int MedicineId { get; set; }
        public string ArabicMedicineName { get; set; }
        public string EnglishMedicineName { get; set; }
        public string? MedicineUrl { get; set; }    
        public int Quantity { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal PriceBeforeDiscount { get; set; }
        public decimal Discount { get; set; }
    }
}
