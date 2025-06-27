using AutoMapper;
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PharmacySystem.ApplicationLayer.Common;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Login;
using PharmacySystem.ApplicationLayer.DTOs.Pharmacy.Register;
using PharmacySystem.ApplicationLayer.IServiceInterfaces;
using PharmacySystem.DomainLayer.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PharmacySystem.ApplicationLayer.Services;

public class PharmacyService : IPharmacyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public PharmacyService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<ValidationResult?> RegisterPharmacyAsync(PharmacyRegisterDto dto)
    {
        var validation = new ValidationResult();

        // Check for duplicate email
        if (await _unitOfWork.PharmacyRepository.EmailExistsAsync(dto.Email))
        {
            validation.Errors.Add("Email", ["Pharmacy with this email already exists."]);
        }

        // Check if Area exists
        var area = await _unitOfWork.AreaRepository.GetByIdAsync(dto.AreaId);
        if (area == null)
        {
            validation.Errors.Add("Area", ["Invalid Area."]);
        }

        // Check if Governate exists
        var governate = _unitOfWork.GovernateRepository
            .FindAsync(g => g.Name.ToLower() == dto.Governate.Trim().ToLower()).FirstOrDefault();
        if (governate == null)
        {
            validation.Errors.Add("Governate", ["Invalid Governate."]);
        }

        // Check if Representative exists
        var representative = _unitOfWork.representativeRepository
            .FindAsync(r => r.Code == dto.RepresentativeCode).FirstOrDefault();
        if (representative == null)
        {
            validation.Errors.Add("RepresentativeId", ["Invalid Representative Code."]);
        }

        // If there are validation errors, return them
        if (validation.HasErrors)
            return validation;

        // Map DTO to entity
        var pharmacy = _mapper.Map<DomainLayer.Entities.Pharmacy>(dto);

        // Set foreign key and hash password
        pharmacy.RepresentativeId = representative!.Id;
        pharmacy.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        await _unitOfWork.PharmacyRepository.AddAsync(pharmacy);
        await _unitOfWork.SaveAsync();

        return null; // Success — no errors
    }

    public async Task<PharmacyLoginResponseDTO> LoginAsync(PharmacyLoginDTO dto)
    {
        // Retrieve the pharmacy entity by email with representative details
        var pharmacy = await _unitOfWork.PharmacyRepository.FindByEmailWithRepresentativeAsync(dto.Email);

        if (pharmacy == null)
            return new PharmacyLoginResponseDTO
            {
                Success = false,
                Message = "Invalid email or password."
            };

        // Verify the password against the stored hash
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, pharmacy.Password))
            return new PharmacyLoginResponseDTO
            {
                Success = false,
                Message = "Invalid email or password."
            };

        // Generate JWT token
        var token = GenerateJwtToken(pharmacy);

        return new PharmacyLoginResponseDTO
        {
            Success = true,
            Message = "Login successful.",
            Token = token,
            Pharmacy = _mapper.Map<PharmacyInfoDto>(pharmacy)

        };
    }

    private string GenerateJwtToken(Pharmacy pharmacy)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, pharmacy.Id.ToString()),
            new Claim(ClaimTypes.Email, pharmacy.Email),
            new Claim(ClaimTypes.Role, "Pharmacy")
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<ValidationResult?> ForgotPasswordAsync(ForgotPasswordRequestDto dto)
    {
        var validation = new ValidationResult();
        var pharmacy = await _unitOfWork.PharmacyRepository.FindByEmailAsync(dto.Email);
        if (pharmacy == null)
        {
            validation.Errors.Add("Email", ["No pharmacy found with this email."]);
            return validation;
        }
        
        // Store the provided OTP (handled by mobile app)
        pharmacy.PasswordResetOTP = dto.OTP;
        pharmacy.PasswordResetOTPExpiry = DateTime.UtcNow.AddMinutes(10); // 10 minutes expiry
        await _unitOfWork.PharmacyRepository.UpdateAsync(pharmacy);
        await _unitOfWork.SaveAsync();
        
        // Send the OTP via email
        var subject = "Pharmacy System Password Reset OTP";
        var body = $"<p>Your password reset OTP is: <strong>{dto.OTP}</strong></p><p>This OTP will expire in 10 minutes.</p>";
        await _emailService.SendEmailAsync(pharmacy.Email, subject, body);
        
        return null; // Success
    }

    public async Task<ValidationResult?> ResetPasswordAsync(ResetPasswordRequestDto dto)
    {
        var validation = new ValidationResult();
        var pharmacy = await _unitOfWork.PharmacyRepository.FindByEmailAsync(dto.Email);
        if (pharmacy == null)
        {
            validation.Errors.Add("Email", ["No pharmacy found with this email."]);
            return validation;
        }
        
        if (pharmacy.PasswordResetOTP != dto.OTP || pharmacy.PasswordResetOTPExpiry == null || pharmacy.PasswordResetOTPExpiry < DateTime.UtcNow)
        {
            validation.Errors.Add("OTP", ["Invalid or expired OTP."]);
            return validation;
        }
        
        if (dto.NewPassword != dto.ConfirmPassword)
        {
            validation.Errors.Add("ConfirmPassword", ["Passwords do not match."]);
            return validation;
        }
        
        // Update password and clear OTP
        pharmacy.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        pharmacy.PasswordResetOTP = null;
        pharmacy.PasswordResetOTPExpiry = null;
        await _unitOfWork.PharmacyRepository.UpdateAsync(pharmacy);
        await _unitOfWork.SaveAsync();
        
        return null; // Success
    }
}