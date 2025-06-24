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

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
