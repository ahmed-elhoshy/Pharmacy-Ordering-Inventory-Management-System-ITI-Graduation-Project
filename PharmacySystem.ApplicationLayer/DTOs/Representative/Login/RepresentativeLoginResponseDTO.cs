using PharmacySystem.ApplicationLayer.DTOs.Representative.Login;

namespace PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login
{
    public class RepresentativeLoginResponseDTO
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public RepresentativeInfoDto Representative { get; set; }
    }
} 