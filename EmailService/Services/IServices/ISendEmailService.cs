using EmailService.Models;

namespace EmailService.IServices
{
    public interface ISendEmailService
    {
        public void SendEmail(EmailInfo emailInfo);
    }
}
