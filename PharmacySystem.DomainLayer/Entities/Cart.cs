using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.DomainLayer.Entities
{
    public class Cart : BaseEntity
    {
        [Required]
        public int PharmacyId { get; set; }      
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        [ForeignKey("PharmacyId")]
        public virtual Pharmacy Pharmacy { get; set; }
        public virtual ICollection<CartWarehouse> CartWarehouses { get; set; } = new HashSet<CartWarehouse>();
    }
}
