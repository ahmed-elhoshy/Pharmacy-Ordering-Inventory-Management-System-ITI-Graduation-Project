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

        [HttpGet("getAllOrder/{pharmacyId:int}")]
        public async Task<IActionResult> returnOrdersByPahrmacyId(int pharmacyId)
        {
            var result = await _orderService.GetOrdersForPharmacy(pharmacyId);
            var successResponse = new CustomResponse<object>("success", result);
            return Ok(successResponse);
        }

        [HttpGet("getAllOrderByStatus/{pharmacyId:int}")]
        public async Task<IActionResult> returnOrdersByPahrmacyIdAndStatus(int pharmacyId,OrderStatus status)
        {
            var result = await _orderService.GetOrdersForPharmacyByStatus(pharmacyId,status);
            var successResponse = new CustomResponse<object>("success", result);
            return Ok(successResponse);
        }

        [HttpGet("getAllOrderDetails/{orderId:int}")]
        public async Task<IActionResult> returnOrderDetails(int orderId)
        {
            var result=await _orderService.GetOrdersDetails(orderId);
            var successResponse = new CustomResponse<object>("success", result);
            return Ok(successResponse);
        }
    }
}
