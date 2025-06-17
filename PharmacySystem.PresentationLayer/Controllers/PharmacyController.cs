using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Register;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.ApplicationLayer.Services;

namespace PharmacySystem.PresentationLayer.Controllers;

[Route("api/[controller]")]
[ApiController]
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
        var result = await _pharmacyService.RegisterAsync(dto);
        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Message);
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
            return BadRequest(result.Message);

        return Ok(result);
    }
}
