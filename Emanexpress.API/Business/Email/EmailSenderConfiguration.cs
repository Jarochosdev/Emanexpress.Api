namespace Emanexpress.API.Business.Email
{
    public class EmailSenderConfiguration
    {
        public string UserName { get; }
        public string Password { get; }

        public EmailSenderConfiguration(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}