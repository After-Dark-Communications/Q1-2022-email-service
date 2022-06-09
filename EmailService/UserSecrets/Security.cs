namespace EmailService.UserSecrets
{
    public class Security
    {
        private string emailAdress;
        private string userName;
        private string password;

        private string kafkaServer;
        private string kafkaGroupId;
        private string kafkaUsername;
        private string kafkaPassword;

        public string EmailAdress { get => emailAdress; set => emailAdress = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }

        public string KafkaServer { get => kafkaServer; set => kafkaServer = value; }
        public string KafkaGroupId { get => kafkaGroupId; set => kafkaGroupId = value; }
        public string KafkaUsername { get => kafkaUsername; set => kafkaUsername = value; }
        public string KafkaPassword { get => kafkaPassword; set => kafkaPassword = value; }
    }
}
