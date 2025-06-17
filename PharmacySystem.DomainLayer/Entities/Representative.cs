
using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.DomainLayer.Entities
{
    public class Representative : BaseEntity
    {
        public string Code { get; set; }

        public string Name {get; set;}

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Governate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
        public virtual ICollection<Pharmacy> pharmacies {get; set;} = new HashSet<Pharmacy>();

        // For Representative accounts
        //public string? ApprovedByAdminId { get; set; }

        //[ForeignKey("ApprovedByAdminId")]
        //public virtual User? ApprovedByAdmin {get; set;}
    }
}
