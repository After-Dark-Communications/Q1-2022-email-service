using EmailService.Models;

namespace EmailService.Services.IServices
{
    public interface IEmailSettingsService
    {
        /// <summary>
        /// Create and return a default email to show on the front end.
        /// </summary>
        /// <returns>default settings for an email</returns>
        EmailSettings GetDefaultEmailSettings();
        /// <summary>
        /// Update the current email settings with new settings
        /// </summary>
        /// <param name="emailSettings">The new email settings</param>
        void UpdateEmailSettings(EmailSettings emailSettings);
    }
}
