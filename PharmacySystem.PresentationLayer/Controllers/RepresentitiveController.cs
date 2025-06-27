#region
using AutoMapper;
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.Common;
using PharmacySystem.ApplicationLayer.DTOs.OrderDetails;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Register;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies;
using PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmaciesOrdersAndOrderDetails;
using PharmacySystem.ApplicationLayer.DTOs.representative.Create;
using PharmacySystem.ApplicationLayer.DTOs.representative.Read;
using PharmacySystem.ApplicationLayer.DTOs.representative.Update;
using PharmacySystem.ApplicationLayer.DTOs.Representative.Login;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.ApplicationLayer.Services;
using PharmacySystem.DomainLayer.Entities;
using PharmacySystem.DomainLayer.Entities.Constants;
#endregion

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
        //[Authorize(Roles = "Admin")]
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
        //[Authorize(Roles = "Admin")]
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
        //[Authorize(Roles = "Admin")]
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
            if (countOfPharmacies == null)
                return NotFound();
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
            {
                return Ok(new RepresentativeLoginResponseDTO
                {
                    Token = null,
                    Message = "Invalid email or password.",
                    Success = false,
                    Representative = new RepresentativeInfoDto
                    {
                        Id = null,

                        Code = null,
                        Name = null,
                        Address = null,
                        Governate = null,
                        Email = null,
                        Phone = null,
                    }
                });
            }
            return Ok(result);
        }
        #endregion

        #region Get Stats for All Orders
        [HttpGet("orders-stats/{representativeId}")]
        [EndpointSummary("Get Stats for All Orders")]
        public async Task<IActionResult> GetOrdersStatsAsync(int representativeId)
        {
            var result = await _service.GetOrdersStatsAsync(representativeId);
            return Ok(result);
        }
        #endregion

        #region Get All Warehouse Paginated with pharmacies orders using representativeId
        [HttpGet("{representativeId}/orders-by-status")]
        [EndpointSummary("Get All Warehouse with pharmacies orders using representativeId And Order State")]
        public async Task<IActionResult> GetOrdersByStatusPaginated(int representativeId,OrderStatus status,
        [FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllOrdersPaginatedByRepresentativeIdAsync(representativeId, status, pageNumber, pageSize);
            return Ok(result);
        }
        #endregion

        #region Forgot Password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] PharmacySystem.ApplicationLayer.DTOs.Representative.Login.ForgotPasswordRequestDto dto)
        {
            var validation = await _service.ForgotPasswordAsync(dto);
            if (validation is not null && validation.HasErrors)
            {
                return Ok(validation.ToErrorResponse());
            }
            return Ok(new { message = "If the email exists, a password reset OTP has been sent.", success = true });
        }
        #endregion

        #region Reset Password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] PharmacySystem.ApplicationLayer.DTOs.Representative.Login.ResetPasswordRequestDto dto)
        {
            var validation = await _service.ResetPasswordAsync(dto);
            if (validation is not null && validation.HasErrors)
            {
                return Ok(validation.ToErrorResponse());
            }
            return Ok(new { message = "Password has been reset successfully.", success = true });
        }
        #endregion
    }
}