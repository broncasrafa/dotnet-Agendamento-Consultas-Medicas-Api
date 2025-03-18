using System.Linq.Expressions;
using System.Security.Claims;

using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;

public interface IAccountManagerService
{
    Task<ApplicationUser> FindByEmailAsync(string email);
    Task<ApplicationUser> FindByUsernameAsync(string username);
    Task<ApplicationUser> FindByFilterAsync(Expression<Func<ApplicationUser, bool>> filter);
    Task<UsuarioAutenticadoModel> LoginAsync(string email, string password);
    Task<UsuarioAutenticadoModel> RegisterAsync(string nomeCompleto, string documento, string username, string email, string telefone, string genero, string password, ETipoPerfilAcesso tipoAcesso);
    Task<ApplicationUser> CheckIfAlreadyExistsByFilterAsync(Expression<Func<ApplicationUser, bool>> filter);
    Task<string> ForgotPasswordAsync(ApplicationUser user);
    Task<bool> ResetPasswordAsync(ApplicationUser user, string resetCode, string newPassword);
    Task<bool> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
    Task<bool> ConfirmEmailAsync(string userId, string code);
    Task<string> ResendEmailConfirmationTokenAsync(string email);
    Task<string> GetEmailConfirmationTokenAsync(string email);
    Task<ApplicationUser> GetUserAsync(ClaimsPrincipal authenticatedUser);
    Task<bool> UpdateUserAsync(ApplicationUser user);
    Task<bool> DeactivateUserAccountAsync(ApplicationUser user);
}