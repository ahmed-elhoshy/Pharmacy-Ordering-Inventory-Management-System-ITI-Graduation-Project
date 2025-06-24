using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.DomainLayer.Common;

namespace PharmacySystem.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        #endregion

        [HttpGet("warehouse/{warehouseId}")]
        public async Task<IActionResult> GetOrdersByWarehouseId(int warehouseId)
        {
            var orders = await _orderService.GetOrdersForWarehouseAsync(warehouseId);
            if (orders == null || !orders.Any())
            {
                var errorResponse = new CustomResponse<object>("fail", null);
                return NotFound(errorResponse);
            }

            var successResponse = new CustomResponse<object>("success", orders);
            return Ok(successResponse);
        }
    }
}
