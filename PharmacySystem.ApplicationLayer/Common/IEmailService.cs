using System.Threading.Tasks;

namespace PharmacySystem.ApplicationLayer.Common
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
} 