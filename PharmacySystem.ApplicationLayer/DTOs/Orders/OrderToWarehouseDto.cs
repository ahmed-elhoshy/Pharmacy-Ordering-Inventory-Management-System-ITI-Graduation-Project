using PharmacySystem.ApplicationLayer.DTOs.Medicines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.Orders
{
    public class OrderToWarehouseDto
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public List<MedicineDto> Medicines { get; set; } = new();
    }
}
