using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.DomainLayer.Entities
{
    public class WareHouse : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Governate { get; set; }
        public bool IsTrusted { get; set; } = false;

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        public virtual ICollection<WareHouseArea> WareHouseAreas { get; set; } = new HashSet<WareHouseArea>();
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<WareHouseMedicien> WareHouseMedicines { get; set; } = new HashSet<WareHouseMedicien>();


        // For Warehouse accounts
        public bool IsWarehouseApproved { get; set; } = false;
        public string? ApprovedByAdminId { get; set; }

    }
}
