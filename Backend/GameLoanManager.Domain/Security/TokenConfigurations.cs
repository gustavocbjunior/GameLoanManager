namespace GameLoanManager.Domain.Security
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpirationMinutes { get; set; }
        public string Key { get; set; }
    }
}