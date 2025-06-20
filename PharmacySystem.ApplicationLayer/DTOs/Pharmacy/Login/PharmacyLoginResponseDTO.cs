namespace PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login
{
    public class PharmacyLoginResponseDTO
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public PharmacyInfoDto Pharmacy { get; set; }
    }
}