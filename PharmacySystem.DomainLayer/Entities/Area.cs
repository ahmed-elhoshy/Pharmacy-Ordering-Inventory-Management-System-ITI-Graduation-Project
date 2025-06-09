using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.DomainLayer.Entities
{
    public class Area : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int GovernateId { get; set; }
        public virtual Governate Governate { get; set; }
        public virtual ICollection<WareHouseArea> WareHouseAreas { get; set; } = new HashSet<WareHouseArea>();
        public virtual ICollection<Pharmacy> Pharmacies { get; set; } = new HashSet<Pharmacy>();
    }
}
