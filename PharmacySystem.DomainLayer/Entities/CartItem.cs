using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PharmacySystem.DomainLayer.Entities
{
    public class CartItem : BaseEntity
    {
        [Required]
        public int CartWarehouseId { get; set; }

        [Required]
        public int MedicineId { get; set; }

        [Required]
        [StringLength(100)]
        public string ArabicMedicineName { get; set; }
        [Required]
        [StringLength(100)]
        public string EnglishMedicineName { get; set; }
        public string? MedicineUrl  { get; set; }
        public string? WarehouseUrl { get; set; } 

        [Required]
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("CartWarehouseId")]
        public virtual CartWarehouse CartWarehouse { get; set; }

        [ForeignKey("MedicineId")]
        public virtual Medicine Medicine { get; set; }
    }
}
