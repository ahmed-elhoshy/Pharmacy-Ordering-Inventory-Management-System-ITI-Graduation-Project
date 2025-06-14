
namespace PharmacySystem.DomainLayer.Entities
{
    public class Representative : BaseEntity
    {
        public string Name {get; set;}
        public string Code {get; set;}
        public string Address {get; set;}
        public string Governate {get; set;}
        public virtual ICollection<Pharmacy> pharmacies {get; set;} = new HashSet<Pharmacy>();

        // For Representative accounts
        //public string? ApprovedByAdminId { get; set; }

        //[ForeignKey("ApprovedByAdminId")]
        //public virtual User? ApprovedByAdmin {get; set;}
    }
}
