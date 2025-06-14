using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.DomainLayer.Entities
{
    public class WareHouse : BaseEntity
    {

        public string? Name { get; set; }

        [Required]
        [StringLength(100)]

        public string Address { get; set; }
        public string Governate { get; set; }
        public bool IsTrusted { get; set; } = false;

        //[ForeignKey("User")]
        //public string UserId { get; set; }
        //public virtual User User { get; set; }
        public virtual ICollection<WareHouseArea> WareHouseAreas { get; set; } = new HashSet<WareHouseArea>();
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<WareHouseMedicien> WareHouseMedicines { get; set; } = new HashSet<WareHouseMedicien>();


         // For Warehouse accounts
        //public bool IsWarehouseApproved { get; set; } = false;
        //public string? ApprovedByAdminId { get; set; }

        //[ForeignKey("ApprovedByAdminId")]
        //public virtual User? ApprovedByAdmin { get; set; }
    }
}
