using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Register;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;

namespace PharmacySystem.PresentationLayer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PharmacyController : ControllerBase
{
    private readonly IPharmacyService _pharmacyService;

    public PharmacyController(IPharmacyService pharmacyService)
    {
        _pharmacyService = pharmacyService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] PharmacyRegisterDto dto)
    {
        var result = await _pharmacyService.RegisterAsync(dto);
        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Message);
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
