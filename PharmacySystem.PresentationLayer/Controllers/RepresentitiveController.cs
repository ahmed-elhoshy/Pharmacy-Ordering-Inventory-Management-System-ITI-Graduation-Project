using AutoMapper;
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Create;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Read;
using PharmacySystem.ApplicationLayer.DTOs.representatitve.Update;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmaciesOrdersAndOrderDetails;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentitiveController : ControllerBase
    {
        #region service
        private readonly IRepresentativeService _service;
        public RepresentitiveController(IRepresentativeService service)
        {
            _service = service;
        }
        #endregion

        #region GetAllRepresentatives
        [HttpGet("GetAllRepresentatives")]
        [EndpointSummary("Get All Representatives")]
        public async Task<IActionResult> GetAll()
        {
            var GetAllRepresentatives = await _service.GetAllAsync();
            return Ok(GetAllRepresentatives);
        }
        #endregion

        #region Get Representatitve By Id
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
        [HttpPost("CreateRepresentatitve")]
        [EndpointSummary("Create a new representative with a unique code")]
        public async Task<IActionResult> Create(CreateRepresentatitveDto dto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Representatitve_Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        #endregion

        #region Update Representatitve
        [HttpPut("UpdateRepresentatitve/{id}")]
        [EndpointSummary("Update Representatitve")]
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
        [HttpDelete("DeleteRepresentatitve/{id}")]
        [EndpointSummary("Delete Representatitve")]
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
        [HttpGet("GetPharmaciesCountUsingCode")]
        [EndpointSummary("Get PharmaciesCount For Each Representatitve Using RepresentatitveCode")]
        public async Task<ActionResult<GetRepresentatitvePharmaciesCountDto>> GetPharmaciesCountUsingCode(string code)
        {
            var PharmaciesCount = await  _service.GetPharmaciesCountByCode(code);
            return Ok(PharmaciesCount);
        }
        #endregion

        #region Get Pharmacies Count Using RepresentatitveId
        [HttpGet("GetOrderCountUsingRepresentatitveId")]
        [EndpointSummary("Get Order Count Using RepresentatitveId")]
        public async Task<ActionResult<GetOrdersPharmaciesCountDto>> GetOrderCountUsingRepresentatitveId(int id)
        {
            var OrderCount = await _service.GetOrdersCountById(id);
            return Ok(OrderCount);
        }
        #endregion
    }
}