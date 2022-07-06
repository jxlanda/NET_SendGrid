using SendGrid;

namespace NET_SendGrid.Models
{
    public interface ICustomEmailSender
    {
        Task<Response> SendEmailAsync(EmailMessage message);
        Task<Response> OnResetPassword(ResetPasswordEventArgs eventArgs);
    }
}
