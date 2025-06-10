using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create
{
    public class CreateWareHouseAreaDTO
    {
        [Required]
        public int AreaId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Minimum price must be a positive value.")]
        public decimal MinmumPrice { get; set; }
    }
}
