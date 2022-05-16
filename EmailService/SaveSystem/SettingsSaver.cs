using EmailService.Models;
using System.Text.Json;

namespace EmailService.SaveSystem
{
    public static class SettingsSaver
    {
        private const string SaveFilePath = "./SaveSystem/SaveFile/emailSettings.ini";

        public static void SaveNewSettings(EmailSettings emailSettings)
        {
            var json = JsonSerializer.SerializeToDocument(emailSettings);

            if (File.Exists(SaveFilePath) == false)
                File.Create(SaveFilePath);

            File.WriteAllText(SaveFilePath, json.ToString());
        }
    }
}
