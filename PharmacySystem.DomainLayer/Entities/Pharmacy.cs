using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.DomainLayer.Entities
{
    public class Pharmacy : BaseEntity
    {

        [StringLength(100)]
        public string Address { get; set; }
        public string Governate { get; set; }

        [ForeignKey("Area")]
        public int AreaId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }


        public virtual Area Area { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        // For Pharmacy accounts
        public int? ApprovalCode { get; set; }
        public bool IsApproved { get; set; } = false;
        public int? ApprovedByRepresentativeId { get; set; }

        [ForeignKey("ApprovedByRepresentativeId")]
        public virtual Representative? ApprovedByRepresentative { get; set; }



    }
}
