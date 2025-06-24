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
        // Step 1: Collect ModelState errors
        var modelErrors = new ValidationResult();
        if (!ModelState.IsValid)
        {
            foreach (var entry in ModelState)
            {
                var key = entry.Key;
                var errors = entry.Value.Errors.Select(e => e.ErrorMessage).ToList();
                if (errors.Any())
                    modelErrors.Errors[key] = errors;
            }
            return Ok(modelErrors.ToErrorResponse());
        }

        // Step 2: Manual validation
        var validation = await _pharmacyService.RegisterPharmacyAsync(dto);
        if (validation is not null && validation.HasErrors)
        {
            return Ok(validation.ToErrorResponse());
        }

        return Ok(new
        {
            message = "Pharmacy registered successfully.",
            success = true
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
