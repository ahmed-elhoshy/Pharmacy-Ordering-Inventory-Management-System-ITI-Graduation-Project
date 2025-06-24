using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.Cart.Read
{
    public class CartDto
    {
        public int TotalQuantity { get; set; }
        public decimal TotalPriceBeforeDisscount { get; set; }
        public decimal TotalPriceAfterDisscount { get; set; }
        public List<CartWarehouseDto> Warehouses { get; set; } = new();
    }
}
