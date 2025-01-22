namespace RSF.AgendamentoConsultas.Infra.Identity.Configurations;

public class JWTSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int DurationInMinutes { get; set; }
    public int DurationInHours { get; set; }
    public int DurationInDays { get; set; }
}