namespace RSF.AgendamentoConsultas.Core.Application.Services.HasherPassword;

public interface IPasswordHasher
{
    string Hash(string password);
}