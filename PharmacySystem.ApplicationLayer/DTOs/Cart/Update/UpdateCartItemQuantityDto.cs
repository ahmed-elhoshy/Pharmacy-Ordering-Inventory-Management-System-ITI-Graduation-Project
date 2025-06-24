
namespace PharmacySystem.ApplicationLayer.DTOs.Cart.Update
{
    public class UpdateCartItemQuantityDto
    {
        public int WarehouseId { get; set; }
        public int PharmcyId { get; set; }
        public int MedicineId { get; set; }
        public int NewQuantity { get; set; }
    }
}
