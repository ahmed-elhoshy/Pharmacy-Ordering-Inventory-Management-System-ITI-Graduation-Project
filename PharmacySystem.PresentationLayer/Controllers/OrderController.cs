using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.DomainLayer.Common;
using PharmacySystem.DomainLayer.Entities.Constants;

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


        [HttpGet("getAllOrderByStatus/{pharmacyId:int}")]
        public async Task<IActionResult> returnOrdersByPahrmacyIdAndStatus(int pharmacyId, [FromQuery] int page = 1,
          [FromQuery] int pageSize = 15, OrderStatus? status = null)
        {
            var result = await _orderService.GetOrdersForPharmacyByStatus(pharmacyId, page,pageSize,status);
            if (result == null || result.Items == null || !result.Items.Any())
            {
                var errorResponse = new CustomResponse<object>("fail", null);
                return Ok(errorResponse);
            }
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            var successResponse = new CustomResponse<object>("success", result);
            return Ok(successResponse);
        }

        [HttpGet("getAllOrderDetails/{orderId:int}")] 
        public async Task<IActionResult> returnOrderDetails(int orderId)
        {
            var result = await _orderService.GetOrdersDetails(orderId);
            var successResponse = new CustomResponse<object>("success", result);
            return Ok(successResponse);
        }
    }
}
