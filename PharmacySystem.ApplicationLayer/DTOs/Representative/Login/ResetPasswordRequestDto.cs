namespace PharmacySystem.ApplicationLayer.DTOs.Representative.Login
{
    public class ResetPasswordRequestDto
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
} 