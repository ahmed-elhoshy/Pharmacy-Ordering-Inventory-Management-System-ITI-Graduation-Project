using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.representative.Read
{
    public class GetRepresentativeByIdDto
    {
        public int Representative_Id { get; set; }
        public string Code { get; set; }
        public string Representatitve_Name { get; set; }
        public string Address { get; set; }
        public string Governate { get; set; }
    }
}
