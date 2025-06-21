using PharmacySystem.DomainLayer.Entities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Read
{
    public class WarehouseMedicineDto
    {
        public int MedicineId { get; set; }
        public string EnglishMedicineName { get; set; }
        public string ArabicMedicineName { get; set; }

        public MedicineTypes Drug { get; set; }

        public decimal price { get; set; }

        public string? MedicineUrl { get; set; }
        public decimal Finalprice { get; set; }

        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
