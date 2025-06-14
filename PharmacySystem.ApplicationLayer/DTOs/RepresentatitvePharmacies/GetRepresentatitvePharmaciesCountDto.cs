
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies
{
    public class GetRepresentatitvePharmaciesCountDto
    {
        public int RepresentatitveId { get; set; }
        public string RepresentatitveName { get; set; }
        public int PharmaciesCount { get; set; }
    }
}
