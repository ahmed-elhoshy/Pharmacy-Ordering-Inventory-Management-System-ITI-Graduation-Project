using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.ApplicationLayer.DTOs.Admin
{
    public class UpdateAdminDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string? Password { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
    }
} 