using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.Services;
using PharmacySystem.DomainLayer.Interfaces;

namespace PharmacySystem.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseService _service;

        public WarehouseController(WarehouseService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Pharmacy")]
        [HttpGet("GetWarhehousesByArea")]
        public async Task<IActionResult> GetWarehousesByArea()
        {
            var areaIdClaim = User.FindFirst("AreaId")?.Value;

            if (string.IsNullOrEmpty(areaIdClaim))
                return Unauthorized("Area ID not found in token");

            if (!int.TryParse(areaIdClaim, out int areaId))
                return BadRequest("Invalid Area ID");

            var warehouses = await _service.GetWarehousesByUserAreaAsync(areaId);

            return Ok(warehouses); 
        }

    }
}
