using AutoMapper;
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Register;
using PharmacySystem.ApplicationLayer.DTOs.representative.Create;
using PharmacySystem.ApplicationLayer.DTOs.representative.Read;
using PharmacySystem.ApplicationLayer.DTOs.representative.Update;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmaciesOrdersAndOrderDetails;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.ApplicationLayer.Services;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentativeController : ControllerBase
    {
        #region service
        private readonly IRepresentativeService _service;
        public RepresentativeController(IRepresentativeService service)
        {
            _service = service;
        }
        #endregion

        #region GetAllRepresentatives
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllRepresentatives")]
        [EndpointSummary("Get All Representatives")]
        public async Task<IActionResult> GetAll()
        {
            var GetAllRepresentatives = await _service.GetAllAsync();
            return Ok(GetAllRepresentatives);
        }
        #endregion

        #region Get Representatitve By Id
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById")]
        [EndpointSummary("Get Representative By ID")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        #endregion

        #region Create Representatitve
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateRepresentative")]
        [EndpointSummary("Create a new representative with a unique code")]
        public async Task<IActionResult> Create(CreateRepresentativeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Representative_Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        #endregion

        #region Update Representatitve
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateRepresentative/{id}")]
        [EndpointSummary("Update Representative")]
        public async Task<IActionResult> Update(int id, UpdateRepresentativeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _service.UpdateAsync(id, dto);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        #endregion

        #region Delete Representatitve
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteRepresentative/{id}")]
        [EndpointSummary("Delete Representative")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _service.DeleteAsync(id)
                ? Ok(new { Message = "Deleted" })
                : NotFound();
        }

        #endregion

        #region Get Pharmacies Count Using Representatitve Code
        [HttpGet("GetPharmaciesCountUsingId")]
        [EndpointSummary("Get PharmaciesCount For Each Representatitve Using RepresentatitveId")]
        public async Task<ActionResult<GetRepresentatitvePharmaciesCountDto>> GetPharmaciesCountUsingId(int id)
        {
            var countOfPharmacies = await  _service.GetPharmaciesCountById(id);
            return Ok(countOfPharmacies);
        }
        #endregion

        #region Get Pharmacies Count Using RepresentatitveId
        [HttpGet("GetOrderCountUsingRepresentativeId")]
        [EndpointSummary("Get Order Count Using RepresentativeId")]
        public async Task<ActionResult<GetOrdersPharmaciesCountDto>> GetOrderCountUsingRepresentatitveId(int id)
        {
            var OrderCount = await _service.GetOrdersCountById(id);
            return Ok(OrderCount);
        }
        #endregion

        #region Representative Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] RepresentativeLoginDTO dto)
        {
            var result = await _service.LoginAsync(dto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
        #endregion

        [HttpGet("orders-stats/{representativeId}")]
        [EndpointSummary("Get Stats for All Orders")]
        public async Task<IActionResult> GetOrdersStatsAsync(int representativeId)
        {
            var result = await _service.GetOrdersStatsAsync(representativeId);
            return Ok(result);
        }

        [HttpGet("warehouse-orders/{representativeId}")]
        [EndpointSummary("Get All Warehouse with pharmacies orders using representativeId")]
        public async Task<IActionResult> GetRepresentativeWarehouseOrdersAsync(int representativeId)
        {
            var result = await _service.GetRepresentativeWarehouseOrdersAsync(representativeId);
            return Ok(result);
        }
    }
}