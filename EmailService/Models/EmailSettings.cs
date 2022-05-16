namespace EmailService.Models
{
    [Serializable]
    public class EmailSettings
    {
        /// <summary>
        /// The path to the logo of the company to be placed at the top or the bottom
        /// </summary>
        public string ImagePath { get => ImagePath; set => ImagePath = value; }
        /// <summary>
        /// Header text that has the following format as an example: "Beste [customername]"
        /// </summary>
        public string MailHeader { get => MailHeader; set => MailHeader = value; }
        /// <summary>
        /// The intro text for the email that will be the first paragraph and a thanks to the customer.
        /// </summary>
        public string MailIntro { get => MailIntro; set => MailIntro = value; }
        /// <summary>
        /// The main body text that contains the link to the survey and the text surrounding it.
        /// </summary>
        public string MailBody { get => MailBody; set => MailBody = value; }
        /// <summary>
        /// The bottom of the page with info about the sender and a thankyou closure message.
        /// </summary>
        public string Footer { get => Footer; set => Footer = value; }
        /// <summary>
        /// Determines the place of the logo of the company can be either top or bottom.
        /// </summary>
        public bool ImageAtTop { get => ImageAtTop; set => ImageAtTop = value; }
    }
}
