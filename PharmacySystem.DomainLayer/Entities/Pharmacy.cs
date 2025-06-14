using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.DomainLayer.Entities
{
    public class Pharmacy : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Governate { get; set; }
        public string RepresentativeCode { get; set; }

        [ForeignKey("Representative")]
        public int RepresentativeId { get; set; }

        [ForeignKey("Area")]
        public int AreaId { get; set; }


        //[ForeignKey("User")]
        //public string UserId { get; set; }

        public virtual Representative Representative { get; set; }
        public virtual Area Area { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        //public virtual User User { get; set; }


        // For Pharmacy accounts
        //public int? ApprovalCode { get; set; }
        //public bool IsApproved { get; set; } = false;
        //public int? ApprovedByRepresentativeId { get; set; }

        //[ForeignKey("ApprovedByRepresentativeId")]
    }
}
