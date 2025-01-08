namespace RSF.AgendamentoConsultas.Application.Services.HasherPassword;

public interface IPasswordHasher
{
    string Hash(string password);
}