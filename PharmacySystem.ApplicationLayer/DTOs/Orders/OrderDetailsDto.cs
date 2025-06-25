using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.Orders
{
    public class OrderDetailsDto
    {
        public string ArabicMedicineName {  get; set; }
        public string MedicineName {  get; set; }
        public int Quantity {  get; set; }
        public decimal TotalPriceAfterDisccount {  get; set; }
        public decimal TotalPriceBeforeDisccount {  get; set; }
        public decimal MedicinePrice {  get; set; }
        public string MedicineImage {  get; set; }
        public decimal DiscountAmount { get; set; }
        
    }
}
