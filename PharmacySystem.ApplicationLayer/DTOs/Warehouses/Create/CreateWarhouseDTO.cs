using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Create;

namespace PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create
{
    public class CreateWarehouseDTO
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }


        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string? Governate { get; set; }

        public bool IsTrusted { get; set; } = false;


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string? ImageUrl { get; set; }
        public bool IsWarehouseApproved { get; set; } = false;
        public string? ApprovedByAdminId { get; set; }
        public List<CreateWareHouseAreaDTO> WareHouseAreas { get; set; } = new();

        public List<CreateWarehouseMedicineDTO> WareHouseMedicines { get; set; } = new();

    }
}
