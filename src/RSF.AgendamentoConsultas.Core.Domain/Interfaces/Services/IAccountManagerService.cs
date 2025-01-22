using RSF.AgendamentoConsultas.Core.Domain.Models;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;

public interface IAccountManagerService
{
    Task<UsuarioAutenticadoModel> LoginAsync(string email, string password);
    Task<UsuarioAutenticadoModel> RegisterAsync(string nomeCompleto, string cpf, string username, string email, string telefone, string password, string tipoAcesso);
    Task<UsuarioAutenticadoModel> CheckIfAlreadyExistsAsync(string email);
}