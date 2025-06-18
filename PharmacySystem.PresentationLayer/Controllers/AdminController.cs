using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.DTOs.representative.Create;
using PharmacySystem.ApplicationLayer.DTOs.representative.Update;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Update;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using System.Threading.Tasks;

namespace PharmacySystem.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #region Representative Endpoints
        [HttpPost("representatives")]
        public async Task<IActionResult> CreateRepresentative([FromBody] CreateRepresentativeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminService.CreateRepresentativeAsync(dto);
            return CreatedAtAction(nameof(GetRepresentative), new { id = result.Representative_Id }, result);
        }

        [HttpPut("representatives/{id}")]
        public async Task<IActionResult> UpdateRepresentative(int id, [FromBody] UpdateRepresentativeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminService.UpdateRepresentativeAsync(id, dto);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("representatives/{id}")]
        public async Task<IActionResult> DeleteRepresentative(int id)
        {
            var result = await _adminService.DeleteRepresentativeAsync(id);
            if (!result)
                return NotFound();

            return Ok(new { Message = "Representative deleted successfully" });
        }

        [HttpGet("representatives")]
        public async Task<IActionResult> GetAllRepresentatives()
        {
            var representatives = await _adminService.GetAllRepresentativesAsync();
            return Ok(representatives);
        }

        [HttpGet("representatives/{id}")]
        public async Task<IActionResult> GetRepresentative(int id)
        {
            var representative = await _adminService.GetRepresentativeByIdAsync(id);
            if (representative == null)
                return NotFound();

            return Ok(representative);
        }
        #endregion

        #region Warehouse Endpoints
        [HttpPost("warehouses")]
        public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminService.CreateWarehouseAsync(dto);
            return CreatedAtAction(nameof(GetWarehouse), new { id = result.Id }, result);
        }

        [HttpPut("warehouses/{id}")]
        public async Task<IActionResult> UpdateWarehouse(int id, [FromBody] UpdateWareHouseDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminService.UpdateWarehouseAsync(id, dto);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("warehouses/{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            var result = await _adminService.DeleteWarehouseAsync(id);
            if (!result)
                return NotFound();

            return Ok(new { Message = "Warehouse deleted successfully" });
        }

        [HttpGet("warehouses")]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var warehouses = await _adminService.GetAllWarehousesAsync();
            return Ok(warehouses);
        }

        [HttpGet("warehouses/{id}")]
        public async Task<IActionResult> GetWarehouse(int id)
        {
            var warehouse = await _adminService.GetWarehouseByIdAsync(id);
            if (warehouse == null)
                return NotFound();

            return Ok(warehouse);
        }
        #endregion
    }
} 