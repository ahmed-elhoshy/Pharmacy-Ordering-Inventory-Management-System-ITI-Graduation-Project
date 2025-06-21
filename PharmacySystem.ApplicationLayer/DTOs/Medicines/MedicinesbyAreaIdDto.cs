using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PharmacySystem.ApplicationLayer.DTOs.Medicines
{
    public class MedicinesbyAreaIdDto
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public decimal Price { get; set; }
        public string MaximumwareHouseAreaName { get; set; }
        public string ImageUrl { get; set; }
        public decimal finalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public int DistributorsCount { get; set; }
        public int WarehouseIdOfMaxDiscount { get; set; }
        public string WarehouseNameOfMaxDiscount { get; set; }
        public int QuantityInWarehouseWithMaxDiscount { get; set; }
        public decimal MaximumDiscount { get; set; }
        public decimal MinmumPrice { get; set; }
    }
}
