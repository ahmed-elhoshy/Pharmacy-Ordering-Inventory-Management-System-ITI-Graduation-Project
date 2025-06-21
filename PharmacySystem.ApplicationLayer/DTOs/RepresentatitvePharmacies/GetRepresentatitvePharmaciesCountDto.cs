
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Read;
using PharmacySystem.DomainLayer.Entities;

namespace PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmacies
{
    public class GetRepresentatitvePharmaciesCountDto
    {
        public int RepresentativeId { get; set; }
        public string RepresentativeName { get; set; }
        public string UserName { get; set; }
        public int PharmaciesCount { get; set; }
        public List<PharmacyDto> Pharmacies { get; set; } = new();
    }
}
