using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.DomainLayer.Entities
{
    public class Representative : BaseEntity
    {
        public int Code { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Governate { get; set; }

        public virtual ICollection<Pharmacy> pharmacies { get; set; } = new HashSet<Pharmacy>();


        // For Representative accounts
        public string? ApprovedByAdminId { get; set; }

        [ForeignKey("ApprovedByAdminId")]
        public virtual User? ApprovedByAdmin { get; set; }




    }
}
