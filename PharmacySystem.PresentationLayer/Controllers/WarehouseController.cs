using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Create;
using PharmacySystem.ApplicationLayer.DTOs.Warehouses.Update;
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

        //[Authorize(Roles = "Pharmacy")]
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var warehouses = await _service.GetAllAsync();
            return Ok(warehouses);
        }

        // GET: api/warehouses/Getbyid/5
        [HttpGet("Getbyid/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid warehouse ID.");

            var warehouse = await _service.GetByIdAsync(id);
            return warehouse is null ? NotFound() : Ok(warehouse);
        }
        // GET: api/warehouses/GetWarehouseDetailsById/5
        [HttpGet("GetWarehouseDetailsById/{id:int}")]
        public async Task<IActionResult> GetWarehouseDetailsById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid warehouse ID.");

            var warehouse = await _service.GetWarehouseByIdDetailsAsync(id);
            return warehouse is null ? NotFound() : Ok(warehouse);
        }
        // GET: api/warehouses/GetWarehousMedicines/5/medicines?page=1&pageSize=10
        [HttpGet("GetWarehousMedicines/{warehouseId:int}/medicines")]
        public async Task<IActionResult> GetWarehouseMedicines(
            int warehouseId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be greater than 0.");

            var result = await _service.GetWarehouseMedicineDtosAsync(warehouseId, page, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarhouseDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdWarehouse = await _service.AddAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdWarehouse.Id },
                createdWarehouse
            );
        }



        // PUT: api/warehouses/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWareHouseDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID and DTO ID do not match.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await _service.WarehouseExistsAsync(id);
            if (!exists)
                return NotFound();

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        // DELETE: api/warehouses/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid warehouse ID.");

            var exists = await _service.WarehouseExistsAsync(id);
            if (!exists)
                return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

    }
}
