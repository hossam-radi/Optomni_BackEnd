namespace Optomni.Utilities.Settings
{
    public class OptmniSettings
    {
        public JWT jwt { get; set; }
        public System System { get; set; }
    }
    public class JWT
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationInDays { get; set; }

    }

    public class System
    {
        public int Page_Size { get; set; }
        public int Access_Token_Life_Time { get; set; }
        public string Admin_Url { get; set; }
    }

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Port { get; set; } = 587; // default smtp port
        public bool UseSSL { get; set; } = true;
    }


}
