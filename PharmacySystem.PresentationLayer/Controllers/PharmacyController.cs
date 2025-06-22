using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.Common;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Register;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.ApplicationLayer.Services;

namespace PharmacySystem.PresentationLayer.Controllers;

[Route("api/[controller]")]
//[ApiController]
public class PharmacyController : ControllerBase
{
    private readonly IPharmacyService _pharmacyService;
    private readonly IGovernateService _governateService;

    public PharmacyController(IPharmacyService pharmacyService, IGovernateService governateService)
    {
        _pharmacyService = pharmacyService;
        _governateService = governateService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] PharmacyRegisterDto dto)
    {
        var (validation, createdPharmacy) = await _pharmacyService.RegisterPharmacyAsync(dto);
        
        if (validation == null)
        {
            return Ok(new
            {
                message = "Pharmacy registered successfully.",
                pharmacy = new
                {
                    id = createdPharmacy.Id,
                    userName = createdPharmacy.UserName,
                    name = createdPharmacy.Name,
                    email = createdPharmacy.Email,
                    address = createdPharmacy.Address,
                    governate = createdPharmacy.Governate,
                    areaId = createdPharmacy.AreaId,
                    phoneNumber = createdPharmacy.PhoneNumber,
                    RepresentativeCode = createdPharmacy.Representative?.Code
                }
            });
           
        }

        return Ok(new
        {
            validation.Errors,
            pharmacy = new
            {
                id = (int?)null,
                userName = (string)null,
                name = (string)null,
                email = (string)null,
                address = (string)null,
                governate = (string)null,
                areaId = (int?)null,
                phoneNumber = (string)null,
                RepresentativeCode = (string)null


            }
        });
    }

    [HttpGet("register")]
    public async Task<IActionResult> Get([FromQuery] int? governateId)
    {
        if (governateId.HasValue)
        {
            var areas = await _governateService.GetAreasByGovernateIdAsync(governateId.Value);
            return Ok(areas);
        }

        var governates = await _governateService.GetGovernatesAsync();
        return Ok(governates);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] PharmacyLoginDTO dto)
    {
        var result = await _pharmacyService.LoginAsync(dto);
        if (!result.Success)
        {
            return Ok(new PharmacyLoginResponseDTO
            {
                Token = null,
                Message = "Invalid email or password.",
                Success = false,
                Pharmacy = new PharmacyInfoDto
                {
                    Id = null,
                    userName =null,
                    Name = null,
                    Email = null,
                    Address = null,
                    Governate = null,
                    AreaId = null,
                    PhoneNumber = null,
                    RepresentativeId = null,
                }
            });
        }
        return Ok(result);
    }
}
