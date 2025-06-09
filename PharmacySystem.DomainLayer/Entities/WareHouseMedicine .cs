using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.DomainLayer.Entities
{
    public class WareHouseMedicien
    {
        public int MedicineId { get; set; }
        public int WareHouseId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Discount { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual WareHouse WareHouse { get; set; }
    }
}
