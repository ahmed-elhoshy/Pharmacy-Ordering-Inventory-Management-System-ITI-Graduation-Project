using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines
{
    public class WareHouseMedicineAreaDto
    {
        
            public int WarehouseId { get; set; }
        public String WarehHouseName { get; set; }
        public int MedicineId { get; set; }
            public string MedicineName { get; set; }
            public decimal MedicinePrice { get; set; }
            public decimal Discount { get; set; }
            public decimal FinalPrice { get; set; }
        public string WareHouseAreaName { get; set; }
        public int Quantity { get; set; }
    }
    
}
