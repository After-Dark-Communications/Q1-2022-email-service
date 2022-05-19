using EmailService.Context.IContexts;
using EmailService.Models;
using MongoDB.Driver;

namespace EmailService.SaveSystem
{
    public class EmailSettingsContext : IEmailSettingsContext
    {
        private const string EmailSettingsDatabaseKey = "";
        private MongoClient client;

        public void Connect()
        {
            client = new MongoClient("mongodb+srv://Hoeleboele:<password>@cluster0.mshnd.mongodb.net/email-settings");

            IMongoDatabase database = client.GetDatabase("email-settings");

            database.CreateCollection("email-settings");

            IMongoCollection<EmailSettings> mongoCollection = database.GetCollection<EmailSettings>("email-settings");

            EmailSettings emailsetting = mongoCollection.Find(e => e.Footer == "").FirstOrDefault();
        }

        public void SaveEmailSettings()
        {

        }

        public EmailSettings GetEmailSettingsById(int id)
        {
            EmailSettings emailSettings = null;

            return emailSettings;
        }
    }
}
