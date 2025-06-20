using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Register;

public class PharmacyRegisterDto
{
    [Required]
    //[StringLength(20, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 20 characters")]
    //[RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "Username can only contain letters, numbers, and underscores")]
    public string UserName { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "Password must contain letters and numbers.")]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }

    [Required, MaxLength(100)]
    public string Address { get; set; }

    [Required, MaxLength(50)]
    public string Governate { get; set; }

    [Required]
    public int AreaId { get; set; }

    [Required]
    public string RepresentativeCode { get; set; }

    [Required, Phone]
    public string PhoneNumber { get; set; }
}
