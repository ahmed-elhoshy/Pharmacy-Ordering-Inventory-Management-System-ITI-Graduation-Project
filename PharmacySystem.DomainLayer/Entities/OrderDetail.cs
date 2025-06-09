
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.DomainLayer.Entities
{
    public class OrderDetail
    {
        public int Quntity { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Medicine")]
        public int MedicineId { get; set; }


        // Navigation properties
        public virtual Order Order { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}
