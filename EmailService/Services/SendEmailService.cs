using EmailService.IServices;
using EmailService.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace EmailService.Services
{
    public class SendEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        SmtpClient smtpClient = new();

        public SendEmailService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            _configuration = configuration;
        }

        public async void SendEmail(EmailInfo emailInfo)
        {
            // string messageBody = SetupMessageBody(emailInfo);
            string messageBody = "";
            MimeMessage mailMessage = SetupMessage(emailInfo, messageBody);

            SendEmail(emailInfo, mailMessage);
        }

        private string SetupMessageBody(EmailInfo emailInfo)
        {
            string templatePath = _environment.WebRootPath
                                        + Path.DirectorySeparatorChar.ToString()
                                        + "templates"
                                        + Path.DirectorySeparatorChar.ToString()
                                        + emailInfo.TemplateFileName;

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = File.OpenText(templatePath))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(builder.HtmlBody, emailInfo.BodyFormat);
            return messageBody;
        }

        private MimeMessage SetupMessage(EmailInfo emailInfo, string messageBody)
        {
            MimeMessage mailMessage = new MimeMessage();

            mailMessage.From.Add(new MailboxAddress(emailInfo.Security.Value.EmailAdress, emailInfo.Security.Value.EmailAdress));

            mailMessage.To.Add(new MailboxAddress(emailInfo.ReceiverAddress, emailInfo.ReceiverAddress));

            mailMessage.Subject = emailInfo.Subject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = "test text"
            };
                
            //mailMessage.Body = new TextPart("html")
            //{
            //    Text = messageBody
            //};

            return mailMessage;
        }

        private void SendEmail(EmailInfo emailInfo, MimeMessage mailMessage)
        {
            smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtpClient.Connect(_configuration["Mailing:Smtp:Url"], int.Parse(_configuration["Mailing:Smtp:Port"]), SecureSocketOptions.None);
            smtpClient.Authenticate(emailInfo.Security.Value.EmailAdress, emailInfo.Security.Value.Password);
            smtpClient.Send(mailMessage);
            smtpClient.Disconnect(true);
        }
    }
}
