using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.Cart.Write
{
    public class AddToCartDto
    {
        public int WarehouseId { get; set; }
        public int PharmacyId { get; set; }
        public int MedicineId { get; set; }
        public string ArabicMedicineName { get; set; }
        public string EnglishMedicinName { get; set; }
        public string? WarehouseUrl { get; set; }
        public string? MedicineUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
