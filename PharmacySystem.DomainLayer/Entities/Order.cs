
using PharmacySystem.DomainLayer.Entities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.DomainLayer.Entities
{
    public class Order : BaseEntity
    {
        public int Quntity { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }

        [ForeignKey("Pharmacy")]
        public int PharmacyId { get; set; }
        [ForeignKey("WareHouse")]
        public int WareHouseId { get; set; }
        
        // Nagigationl Property
        public virtual Pharmacy Pharmacy { get; set; }
        public virtual WareHouse WareHouse { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
    }
}
