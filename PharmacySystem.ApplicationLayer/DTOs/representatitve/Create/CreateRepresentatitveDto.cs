
using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.ApplicationLayer.DTOs.representatitve.Create
{
    public class CreateRepresentatitveDto
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [StringLength(20)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Governate is required")]
        [StringLength(50)]
        public string Governate { get; set; }
    }
}
