namespace AthenaService.Configuration
{
    public class TokenSettings
    {
        public TimeSpan? TokenLifetime { get; set; }
        public string SecretKeyName { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
