using PharmacySystem.ApplicationLayer.DTOs.Cart.Read;
using PharmacySystem.ApplicationLayer.DTOs.Cart.Write;
using PharmacySystem.DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.IServiceInterfaces
{
    public interface ICartService
    {
        Task<CartDto?> GetCartAsync(int pharmacyId);
        Task<OperationResult<bool>> AddToCartAsync(AddToCartDto request);
        Task<OperationResult<bool>> PlaceOrderAsync(int pharmacyId);
        Task<OperationResult<bool>> UpdateCartItemQuantityAsync(int pharmacyId, int warehouseId, int medicineId, int newQuantity);
        Task<OperationResult<bool>> RemoveCartItemAsync(int pharmacyId, int warehouseId, int medicineId);
        Task<OperationResult<bool>> RemoveWarehouseFromCartAsync(int pharmacyId, int warehouseId);
    }
}
