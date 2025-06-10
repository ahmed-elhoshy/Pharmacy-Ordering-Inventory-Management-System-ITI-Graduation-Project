using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.Warehouses.Read
{
    public class ReadWareHouseAreaDTO
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public decimal MinmumPrice { get; set; }
    }
}
