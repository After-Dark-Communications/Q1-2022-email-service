using EmailService.Models;

namespace EmailService.Context.IContexts
{
    public interface IEmailSettingsContext
    {
        public void SaveEmailSettings();

        public EmailSettings GetEmailSettingsById(int id);
    }
}
