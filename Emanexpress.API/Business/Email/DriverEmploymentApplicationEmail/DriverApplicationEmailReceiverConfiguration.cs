namespace Emanexpress.API.Business.Email
{
    public class DriverApplicationEmailReceiverConfiguration
    {
        public string Email { get; }        

        public DriverApplicationEmailReceiverConfiguration(string email)
        {
            Email = email;            
        }
    }
}