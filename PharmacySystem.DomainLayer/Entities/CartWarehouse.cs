using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.DomainLayer.Entities
{
    public class CartWarehouse : BaseEntity
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int WareHouseId { get; set; }

        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }

        [ForeignKey("WareHouseId")]
        public virtual WareHouse WareHouse { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
