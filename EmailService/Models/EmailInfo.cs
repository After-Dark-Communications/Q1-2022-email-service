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
        public string TemplateFileName { get => templateFileName; set => templateFileName = value; }
        public object[] BodyFormat { get => bodyFormat; set => bodyFormat = value; }

        public IOptions<Security> Security => security;
    }
}
