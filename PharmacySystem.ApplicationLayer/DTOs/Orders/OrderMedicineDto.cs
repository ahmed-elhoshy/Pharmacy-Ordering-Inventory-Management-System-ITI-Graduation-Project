using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.Orders
{
    public class OrderMedicineDto
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string WareHouseName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string WareHouseImage { get; set; }
        public List<OrderDetailsDto> Details { get; set; }
    }
}
