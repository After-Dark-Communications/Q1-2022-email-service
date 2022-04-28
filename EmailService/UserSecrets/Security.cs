namespace EmailService.UserSecrets
{
    public class Security
    {
        private string emailAdress;
        private string userName;
        private string password;

        public string EmailAdress { get => emailAdress; set => emailAdress = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
    }
}
