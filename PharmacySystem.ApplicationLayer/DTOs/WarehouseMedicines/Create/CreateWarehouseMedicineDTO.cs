using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines.Create
{
    public class CreateWarehouseMedicineDTO
    {
        [Required]
        public int MedicineId { get; set; }
        [Required]
        [Range(0, int.MaxValue,ErrorMessage = "Please Put a valid Quantity ")]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Discount { get; set; }
    }
}
