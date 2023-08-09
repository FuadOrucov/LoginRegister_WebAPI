using DYNAMIC_BOX_WebAPI.Helper;

namespace DYNAMIC_BOX_WebAPI.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
