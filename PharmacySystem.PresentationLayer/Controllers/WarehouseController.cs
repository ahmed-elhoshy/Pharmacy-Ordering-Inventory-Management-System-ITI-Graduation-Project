using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Warehouse.Login;
using PharmacySystem.ApplicationLayer.DTOs.WarehouseMedicines;
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
        //GET /api/warehouse/GetWarehousesByArea/5?page=1&pageSize=10&search=central

        [HttpGet("GetWarehousesByArea/{areaId:int}")]
        public async Task<IActionResult> GetWarehousesByArea(
            int areaId,
           [FromQuery] int page = 1,
           [FromQuery] int pageSize = 10,
           [FromQuery] string? search = null)
        {
            if (areaId <= 0)
                return BadRequest("Invalid Area ID");
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var warehouses = await _service.GetWarehousesByUserAreaAsync(page, pageSize, areaId, search);
            return Ok(warehouses);
        }
        //GET /api/warehouse/GetAll
        [HttpGet("GetAll")]
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
            int warehouseId, string? search , string type  , [FromQuery] int page = 1, [FromQuery] int pageSize = 10 )
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be greater than 0.");

            var result = await _service.GetWarehouseMedicineDtosAsync(warehouseId, page, pageSize , search , type);
            return Ok(result);
        }

        [HttpPost("CreateWareHouse")]
        [EndpointSummary("Create a new warehouse with the provided details.")]
        public async Task<IActionResult> Create([FromBody] CreateWarehouseDTO dto)
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

        [EndpointSummary("Get WareHouses that distribute medicine exists in area")]
        [HttpGet("area/{areaId}/medicine/{medicineId}")]
        public async Task<IActionResult> GetWarehousesByAreaAndMedicine(int areaId, int medicineId)
        {
            if (areaId <= 0 || medicineId <= 0)
            {
                return BadRequest("Area ID and Medicine ID must be positive integers");
            }

            var result = await _service.GetWarehousesByAreaAndMedicineAsync(areaId, medicineId);

            if (result == null || !result.Any())
            {
                return NotFound("No warehouses found with the specified criteria");
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] WarehouseLoginDTO dto)
        {
            var result = await _service.LoginAsync(dto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
