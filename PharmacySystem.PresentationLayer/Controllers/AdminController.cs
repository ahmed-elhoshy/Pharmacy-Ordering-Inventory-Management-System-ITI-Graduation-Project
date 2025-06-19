using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.DTOs.Admin;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
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

        #region Admin Endpoints
        [HttpPost("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _adminService.CreateAdminAsync(dto);
                return CreatedAtAction(nameof(GetAdmin), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("UpdateAdmin{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] UpdateAdminDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _adminService.UpdateAdminAsync(id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteAdmin/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var result = await _adminService.DeleteAdminAsync(id);
            if (!result)
                return NotFound();

            return Ok(new { Message = "Admin deleted successfully" });
        }

        [HttpGet("GetALlAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _adminService.GetAllAdminsAsync();
            return Ok(admins);
        }

        [HttpGet("GetAdminByID/{id}")]
        public async Task<IActionResult> GetAdmin(int id)
        {
            try
            {
                var admin = await _adminService.GetAdminByIdAsync(id);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        #endregion

        #region Representative Endpoints
        //[HttpPost("CreateRepresentative")]
        //public async Task<IActionResult> CreateRepresentative([FromBody] CreateRepresentativeDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    try
        //    {
        //        var result = await _adminService.CreateRepresentativeAsync(dto);
        //        return CreatedAtAction(nameof(GetRepresentative), new { id = result.Representative_Id }, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Message = ex.Message });
        //    }
        //}

        //[HttpPut("UpdateRepresentative/{id}")]
        //public async Task<IActionResult> UpdateRepresentative(int id, [FromBody] UpdateRepresentativeDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _adminService.UpdateRepresentativeAsync(id, dto);
        //    if (result == null)
        //        return NotFound();

        //    return Ok(result);
        //}

        //[HttpDelete("DeleteRepresentative/{id}")]
        //public async Task<IActionResult> DeleteRepresentative(int id)
        //{
        //    var result = await _adminService.DeleteRepresentativeAsync(id);
        //    if (!result)
        //        return NotFound();

        //    return Ok(new { Message = "Representative deleted successfully" });
        //}

        //[HttpGet("GetAllRepresentatives")]
        //public async Task<IActionResult> GetAllRepresentatives()
        //{
        //    var representatives = await _adminService.GetAllRepresentativesAsync();
        //    return Ok(representatives);
        //}

        //[HttpGet("GetRepresentativeById/{id}")]
        //public async Task<IActionResult> GetRepresentative(int id)
        //{
        //    var representative = await _adminService.GetRepresentativeByIdAsync(id);
        //    if (representative == null)
        //        return NotFound();

        //    return Ok(representative);
        //}
        #endregion

        #region Warehouse Endpoints
        //[HttpPost("CreateWarehouse")]
        //public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _adminService.CreateWarehouseAsync(dto);
        //    return CreatedAtAction(nameof(GetWarehouse), new { id = result.Id }, result);
        //}

        //[HttpPut("UpdateWarehouse/{id}")]
        //public async Task<IActionResult> UpdateWarehouse(int id, [FromBody] UpdateWareHouseDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _adminService.UpdateWarehouseAsync(id, dto);
        //    if (result == null)
        //        return NotFound();

        //    return Ok(result);
        //}

        //[HttpDelete("DeleteWarehouse/{id}")]
        //public async Task<IActionResult> DeleteWarehouse(int id)
        //{
        //    var result = await _adminService.DeleteWarehouseAsync(id);
        //    if (!result)
        //        return NotFound();

        //    return Ok(new { Message = "Warehouse deleted successfully" });
        //}

        //[HttpGet("GetAllWarehouses")]
        //public async Task<IActionResult> GetAllWarehouses()
        //{
        //    var warehouses = await _adminService.GetAllWarehousesAsync();
        //    return Ok(warehouses);
        //}

        //[HttpGet("GetWarehouseById/{id}")]
        //public async Task<IActionResult> GetWarehouse(int id)
        //{
        //    var warehouse = await _adminService.GetWarehouseByIdAsync(id);
        //    if (warehouse == null)
        //        return NotFound();

        //    return Ok(warehouse);
        //}
        #endregion

        #region Admin Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginDTO dto)
        {
            var result = await _adminService.LoginAsync(dto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
        #endregion
    }
}