using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login
{
    public class RepresentativeLoginDTO
    {

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain letters and numbers.")]
        public string Password { get; set; }
    }
}
