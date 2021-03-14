namespace Emanexpress.API.Business.Email
{
    public class EmailSenderConfiguration
    {
        public string UserName { get; }
        public string Password { get; }
        public string EmailBcc { get; }

        public EmailSenderConfiguration(string userName, string password, string emailBcc)
        {
            UserName = userName;
            Password = password;
            EmailBcc = emailBcc;
        }
    }
}