using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PharmacySystem.DomainLayer.Entities
{
    public class Governate : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Area> Areas { get; set; } = new HashSet<Area>();
    }
}
