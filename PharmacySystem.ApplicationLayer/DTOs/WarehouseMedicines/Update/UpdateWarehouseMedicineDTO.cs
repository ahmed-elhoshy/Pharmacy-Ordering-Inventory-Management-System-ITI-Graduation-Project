using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Update
{
    public class UpdateWarehouseMedicineDTO
    {
        [Required]
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Discount { get; set; }
    }
}
