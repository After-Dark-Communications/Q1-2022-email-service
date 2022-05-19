using EmailService.IServices;
using EmailService.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using RestSharp;
using System.Net;

namespace EmailService.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        //SmtpClient smtpClient = new();

        public SendEmailService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            _configuration = configuration;
        }

        public void SendEmail(EmailInfo emailInfo)
        {
            // string messageBody = SetupMessageBody(emailInfo);
            string messageBody = "https://www.dupuis.com/v5/img/visuels_resume/LL.jpg";
            MimeMessage mailMessage = SetupMessage(emailInfo, messageBody);

            SendEmail(emailInfo, mailMessage);
        }

        private MimeMessage SetupMessage(EmailInfo emailInfo, string url)
        {
            MimeMessage mailMessage = new MimeMessage();

            mailMessage.From.Add(new MailboxAddress(emailInfo.Security.Value.EmailAdress, emailInfo.Security.Value.EmailAdress));

            mailMessage.To.Add(new MailboxAddress(emailInfo.ReceiverAddress, emailInfo.ReceiverAddress));

            mailMessage.Subject = emailInfo.Subject;
            BodyBuilder builder = new BodyBuilder();
            builder.HtmlBody = emailInfo.GetHTMLTemplate(new string[2] { emailInfo.Username ,"https://www.dupuis.com/v5/img/visuels_resume/LL.jpg"});
            mailMessage.Body = builder.ToMessageBody();
            //mailMessage.Body = new TextPart("plain")
            //{
            //    Text = "heey steef dit is een automatische email. de groentenboer!"
            //};

            return mailMessage;
        }

        private void SendEmail(EmailInfo emailInfo, MimeMessage mailMessage)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_configuration["Mailing:Smtp:Url"], int.Parse(_configuration["Mailing:Smtp:Port"]));
                smtpClient.Authenticate(emailInfo.Security.Value.UserName, emailInfo.Security.Value.Password);
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
    }
}
