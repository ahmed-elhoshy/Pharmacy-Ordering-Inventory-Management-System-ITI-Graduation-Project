using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.ApplicationLayer.DTOs.Representative.Login;

public class RepresentativeInfoDto
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Governate { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string Phone { get; set; }
}
