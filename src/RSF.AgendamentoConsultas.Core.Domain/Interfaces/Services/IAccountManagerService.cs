using System.Linq.Expressions;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;

public interface IAccountManagerService
{
    Task<ApplicationUser> FindByEmailAsync(string email);
    Task<ApplicationUser> FindByFilterAsync(Expression<Func<ApplicationUser, bool>> filter);
    Task<UsuarioAutenticadoModel> LoginAsync(string email, string password);
    Task<UsuarioAutenticadoModel> RegisterAsync(string nomeCompleto, string documento, string username, string email, string telefone, string genero, string password, ETipoPerfilAcesso tipoAcesso);
    Task<ApplicationUser> CheckIfAlreadyExistsByFilterAsync(Expression<Func<ApplicationUser, bool>> filter);
    Task<string> ForgotPasswordAsync(ApplicationUser user);
    Task<bool> ResetPasswordAsync(ApplicationUser user, string resetCode, string newPassword);
}