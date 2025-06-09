using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PharmacySystem.DomainLayer.Entities
{
    public class WareHouseArea
    {
        public int WareHouseId { get; set; }
        public int AreaId { get; set; }

        public decimal MinmumPrice { get; set; }

        public virtual WareHouse WareHouse { get; set; } 
        public virtual Area Area { get; set; } 
    }
}
