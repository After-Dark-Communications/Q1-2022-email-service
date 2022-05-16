using EmailService.Models;

namespace EmailService.Factories
{
    public static class EmailSettingsFactory
    {
        public static EmailSettings GetDefaultEmailSettings()
        {
            EmailSettings emailSettings = new EmailSettings()
            {   
                ImageAtTop = true,
                ImagePath = "",

                MailHeader = "",
                MailIntro = "",
                MailBody = ""
            };

            return emailSettings;
        }

        public static EmailSettings GetEmptyEmailSettings()
        {
            return new EmailSettings();
        }
    }
}
