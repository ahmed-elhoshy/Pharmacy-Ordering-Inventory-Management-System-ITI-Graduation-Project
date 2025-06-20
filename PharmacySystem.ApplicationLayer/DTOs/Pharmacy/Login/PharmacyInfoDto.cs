using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;

public class PharmacyInfoDto
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, MaxLength(100)]
    public string Address { get; set; }

    [Required, MaxLength(50)]
    public string Governate { get; set; }

    [Required]
    public int AreaId { get; set; }

    [Required, Phone]
    public string PhoneNumber { get; set; }
}
