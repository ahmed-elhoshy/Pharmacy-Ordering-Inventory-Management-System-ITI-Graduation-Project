using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Update;

namespace PharmacySystem.ApplicationLayer.DTOs.Warehouses.Update
{
    public class UpdateWareHouseDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string? Governate { get; set; }

        public bool IsTrusted { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool IsWarehouseApproved { get; set; }
        public string? ApprovedByAdminId { get; set; }

        public List<UpdateWareHouseAreaDTO> WareHouseAreas { get; set; }
        public List<UpdateWarehouseMedicineDTO> WareHouseMedicines { get; set; }
    }
}
