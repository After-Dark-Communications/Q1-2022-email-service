using EmailService.Factories;
using EmailService.Models;

namespace EmailService.Services.IServices
{
    public class EmailSettingsService : IEmailSettingsService
    {
        public EmailSettings GetDefaultEmailSettings()
        {
            EmailSettings settings = EmailSettingsFactory.GetDefaultEmailSettings();

            if (settings == null)
                throw new Exception("Settings cannot be created something went wrong");

            return settings;
        }

        public void UpdateEmailSettings(EmailSettings emailSettings)
        {
            //TODO: save email settings
        }
    }
}
