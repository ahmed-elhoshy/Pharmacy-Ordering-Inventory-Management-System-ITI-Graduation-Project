using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.DomainLayer.Entities
{
    public class WareHouseMedicien
    {
        public int MedicineId { get; set; }
        public int WareHouseId { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual WareHouse WareHouse { get; set; }
    }
}
