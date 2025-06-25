using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.DTOs.Cart.Write;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using System.Security.Claims;

namespace PharmacySystem.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        #region Constructor
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        #endregion

        #region GetCart
        [HttpGet("{pharmacyId}")]
        public async Task<IActionResult> GetCart(int pharmacyId)
        {
            var cart = await _cartService.GetCartAsync(pharmacyId);

            return Ok(new
            {
                success = cart != null,
                message = cart != null ? "Cart retrieved successfully" : "Cart not found",
                data = cart
            });
        }
        #endregion

        #region AddToCart
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartDto request)
        {
            var result = await _cartService.AddToCartAsync(request);

            return Ok(new
            {
                success = result.IsSuccess,
                message = result.IsSuccess ? "Item added to cart successfully" : result.ErrorMessage,
                data = result.IsSuccess ? (Object?)result.Data : null
            });
        }
        #endregion

        #region PlaceOrder
        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromQuery]  int pharmacyId , [FromQuery] int ? warehouseId = null)
        {
            var result = await _cartService.PlaceOrderAsync(pharmacyId , warehouseId);

            return Ok(new
            {
                success = result.IsSuccess,
                message = result.IsSuccess ? "Order placed successfully" : result.ErrorMessage,
                data = result.IsSuccess ? (object?)result.Data : null
            });
        }
        #endregion

        #region UpdateQuantity
        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateQuantity([FromQuery] int pharmacyId, [FromQuery] int warehouseId, [FromQuery] int medicineId, [FromQuery] int newQuantity)
        {
            var result = await _cartService.UpdateCartItemQuantityAsync(pharmacyId, warehouseId, medicineId, newQuantity);

            return Ok(new
            {
                success = result.IsSuccess,
                message = result.IsSuccess ? "Quantity updated successfully" : result.ErrorMessage,
                data = result.Data
            });
        }
        #endregion

        #region RemoveCartItem
        [HttpDelete("remove-item")]
        public async Task<IActionResult> RemoveCartItem([FromQuery] int pharmacyId, [FromQuery] int warehouseId, [FromQuery] int medicineId)
        {
            var result = await _cartService.RemoveCartItemAsync(pharmacyId, warehouseId, medicineId);

            return Ok(new
            {
                success = result.IsSuccess,
                message = result.IsSuccess ? "Item removed successfully" : result.ErrorMessage,
                data = result.Data
            });
        }
        #endregion

        #region RemoveWarehouseFromCart
        [HttpDelete("remove-warehouse")]
        public async Task<IActionResult> RemoveWarehouseFromCart([FromQuery] int pharmacyId, [FromQuery] int warehouseId)
        {
            var result = await _cartService.RemoveWarehouseFromCartAsync(pharmacyId, warehouseId);

            return Ok(new
            {
                success = result.IsSuccess,
                message = result.IsSuccess ? "Warehouse removed successfully" : result.ErrorMessage,
                data = result.Data
            });
        }
        #endregion

    }
}
