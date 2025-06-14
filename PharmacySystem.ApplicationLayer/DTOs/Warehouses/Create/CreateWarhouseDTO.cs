﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Create;

namespace PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create
{
    public class CreateWarhouseDTO
    {
        
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string? Governate { get; set; }

        public string? Name { get; set; }
        public bool IsTrusted { get; set; } = false;

        [Required]
        public string UserId { get; set; }

        public List<CreateWareHouseAreaDTO> WareHouseAreas { get; set; } = new();

        public List<CreateWarehouseMedicineDTO> WareHouseMedicines { get; set; } = new();

    }
}
