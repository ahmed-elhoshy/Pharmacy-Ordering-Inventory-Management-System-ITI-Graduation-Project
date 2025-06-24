using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.Cart.Read
{
    public class CartWarehouseDto
    {
        public int WarehouseId { get; set; }
        public string? WarehouseUrl { get; set; }   
        public List<CartItemDto> Items { get; set; } = new();
    }
}
