using EmailService.UserSecrets;
using Microsoft.Extensions.Options;

namespace EmailService.Models
{
    [Serializable]
    public class EmailInfo
    {
        private readonly IOptions<Security> security;
        public EmailInfo(IOptions<Security> security)
        {
            this.security = security;
        }

        private string receiverAddress;
        private string subject;
        private string templateFileName;
        private object[] bodyFormat;

        public string ReceiverAddress { get => receiverAddress; set => receiverAddress = value; }
        public string Subject { get => subject; set => subject = value; }
        public string TemplateFilePath { get => templateFileName; set => templateFileName = value; }
        public object[] BodyFormat { get => bodyFormat; set => bodyFormat = value; }

        public IOptions<Security> Security => security;
        
        /// <summary>Generates an html email body as a string</summary>
        /// <param name="args">The variables to put inside the body.</param>
        public string GetHTMLTemplate(params string[] args)
        {
            if (!File.Exists(TemplateFilePath))
            {
                return $"<!DOCTYPE html><html><head></head><body style=\"background-color: #000;\">" +
                    "<div style=\"margin: 10px 50px 10px 50px; padding: 10px 10px 10px 10px; background-color: #fff;\">" +
                    "<div style=\"text-align: center;\">" +
                    "<img src=https://www.dinnerinmotion.nl/wp-content/uploads/2019/08/Logo-Dinner-in-Motion-White-RGB-2000X2000.png width=\"114\" />" +
                    "<p style=\"font-family: Raleway, serif; font-size: 16px;\">" +
                    "Dank u zeer voor het eten bij Dinner in Motion.</p>" +
                    "<p style=\"font-family: Raleway, serif; font-size: 16px;\">" +
                    "Zou u de tijd willen nemen om deze korte vragenlijst in te vullen?" +
                    "Uw mening is van groot belang voor ons." +
                    "Klik <a href=\"" + args[0] + "\">hier</a> om bij de vragenlijst te komen.</p>" +
                    "</div></div></body></html>";
            }
            else
            {
                string body = File.ReadAllText(TemplateFilePath);
                return string.Format(body, args);
            }
        }
    }
}
