using EmailService.Models;

namespace EmailService.IServices
{
    public interface IEmailService
    {
        public void SendEmail(EmailInfo emailInfo);
    }
}
