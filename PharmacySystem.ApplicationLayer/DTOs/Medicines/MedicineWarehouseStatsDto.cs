using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PharmacySystem.ApplicationLayer.DTOs.Medicines
{
    public class MedicineWarehouseStatsDto
    {
        public int MedicineId { get; set; }
        public int NumberOfDistributions { get; set; }
        public int TotalQuantitiesInAllWarehouses { get; set; }
        public decimal MaxDiscount { get; set; }
        public decimal MinDiscount { get; set; }
        public string MedicineName { get; set; }
        public decimal Price { get; set; }
    }
}
