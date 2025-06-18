using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.ApplicationLayer.DTOs.Admin
{
    public class CreateAdminDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

    }
} 