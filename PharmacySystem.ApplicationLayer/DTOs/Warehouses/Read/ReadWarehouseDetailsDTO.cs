using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Read;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.ApplicationLayer.DTOs.Warehouses.Read
{
    public class ReadWarehouseDetailsDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Address { get; set; }
        public string Governate { get; set; }
        public bool IsTrusted { get; set; }
        public string UserName { get; set; }
        public bool IsWarehouseApproved { get; set; }
        public string? ApprovedByAdminName { get; set; }

        public List<string> AreaNames { get; set; } = new();
        public List<WarehouseMedicineDto> Medicines { get; set; } = new();
    }
}
