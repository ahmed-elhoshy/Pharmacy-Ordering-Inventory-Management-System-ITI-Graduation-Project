using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Read
{
    public class PharmacyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Governate { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
    }
}
