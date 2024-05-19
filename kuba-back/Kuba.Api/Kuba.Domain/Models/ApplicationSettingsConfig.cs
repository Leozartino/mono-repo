namespace Kuba.Domain.Models
{
    public class ApplicationSettingsConfig
    {
        public ConnectionStrings? ConnectionStrings { get; set; }
        public JwtSettings? JwtSettings { get; set; }
    }

    public class JwtSettings
    {
        public string? Secret { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }

    public class ConnectionStrings
    {
        public string? DefaultConnection { get; set; }
    }
}

