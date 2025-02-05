using System.Text;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;
using RSF.AgendamentoConsultas.Infra.Identity.Exceptions;

namespace RSF.AgendamentoConsultas.Infra.Identity.AccountManager;

public class AccountManagerService : IAccountManagerService
{
    private readonly ILogger<AccountManagerService> _logger;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public AccountManagerService(
        ILogger<AccountManagerService> logger,
        IJwtTokenService jwtTokenService,
        UserManager<ApplicationUser> userManager,
        IEspecialistaRepository especialistaRepository,
        IPacienteRepository pacienteRepository)
    {
        _logger = logger;
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;
        _especialistaRepository = especialistaRepository;
        _pacienteRepository = pacienteRepository;
    }


    public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal authenticatedUser)
        => await _userManager.GetUserAsync(authenticatedUser);

    public async Task<ApplicationUser> CheckIfAlreadyExistsByFilterAsync(Expression<Func<ApplicationUser, bool>> filter)
        => await _userManager.Users.SingleOrDefaultAsync(filter);

    public async Task<ApplicationUser> FindByEmailAsync(string email) 
        => await _userManager.FindByEmailAsync(email);

    public async Task<ApplicationUser> FindByFilterAsync(Expression<Func<ApplicationUser, bool>> filter)
        => await _userManager.Users.SingleOrDefaultAsync(filter);

    public async Task<string> ForgotPasswordAsync(ApplicationUser user)
        => await _userManager.GeneratePasswordResetTokenAsync(user);
    
    public async Task<bool> ResetPasswordAsync(ApplicationUser user, string resetCode, string newPassword)
    {
        var result = await _userManager.ResetPasswordAsync(user, resetCode, newPassword);
        return result.Succeeded;
    }

    public async Task<bool> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
    {
        _logger.LogInformation("Usuário {UserId} iniciou a alteração de senha.", user.Id);

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogWarning("Usuário {UserId} falhou ao alterar a senha. Erros: {Errors}", user.Id, errors);
            IdentityOperationErrorsException.ThrowIfErrors($"Falha ao alterar a senha do usuário '{user.Id}': {errors})");            
        }


        _logger.LogInformation("Usuário {UserId} alterou a senha com sucesso.", user.Id);

        // invalida os tokens ativos, forçando o logout em dispositivos conectados.
        await _userManager.UpdateSecurityStampAsync(user);

        return result.Succeeded;
    }

    public async Task<UsuarioAutenticadoModel> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        NotFoundException.ThrowIfNull(user, $"Usuário com o e-mail '{email}' não encontrado");

        var isValidCredentials = await _userManager.CheckPasswordAsync(user, password);
        InvalidCredentialsException.ThrowIfNotValid(isValidCredentials);

        int id = 0;
        var especialista = await _especialistaRepository.GetByUserIdAsync(user.Id);
        if (especialista is not null)
            id = especialista.EspecialistaId;
        else
        {
            var paciente = await _pacienteRepository.GetByUserIdAsync(user.Id);
            if (paciente is not null)
                id = paciente.PacienteId;
        }

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);

        var token = _jwtTokenService.GenerateTokenJwt(user, id, roleClaims, userClaims);

        var response = UsuarioAutenticadoModel.MapFromEntity(user, token);

        return response;
    }

    public async Task<UsuarioAutenticadoModel> RegisterAsync(string nomeCompleto, string documento, string username, string email, string telefone, string genero, string password, ETipoPerfilAcesso tipoAcesso)
    {
        var user = await _userManager.FindByEmailAsync(email);
        AlreadyExistsException.ThrowIfExists(user, "Usuário já cadastrado");

        var newUser = new ApplicationUser(nomeCompleto, username, documento, email, genero, telefone.RemoverFormatacaoSomenteNumeros());

        var result = await _userManager.CreateAsync(newUser, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, tipoAcesso.ToString());

            return UsuarioAutenticadoModel.MapFromEntity(newUser);
        }

        _logger.LogError($"Erro ao criar usuário: {string.Join(", ", result.Errors.Select(x => x.Description))}");
        
        return default!;
    }

    public async Task<bool> ConfirmEmailAsync(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);
        NotFoundException.ThrowIfNull(user, $"Usuário com o ID: '{userId}' não encontrado");

        var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, decodedCode);

        return result.Succeeded;
    }

    public async Task<string> ResendEmailConfirmationTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        NotFoundException.ThrowIfNull(user, $"Usuário com o e-mail: '{email}' não encontrado");

        AlreadyExistsException.ThrowIfExists(await _userManager.IsEmailConfirmedAsync(user), $"O e-mail '{email}' já foi confirmado.");
        
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<string> GetEmailConfirmationTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        NotFoundException.ThrowIfNull(user, $"Usuário com o e-mail: '{email}' não encontrado");

        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<bool> UpdateUserAsync(ApplicationUser user)
    {
        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var errors = string.Join("; ", updateResult.Errors.Select(e => e.Description));
            _logger.LogWarning("Erro ao atualizar os dados do usuário {UserId}: {Errors}", user.Id, errors);

            IdentityOperationErrorsException.ThrowIfErrors($"Falha ao alterar os dados do usuário '{user.Id}': {errors})");
        }

        _logger.LogInformation("Usuário {UserId} atualizado com sucesso.", user.Id);
        return true;
    }

    public async Task<bool> DeactivateUserAccountAsync(ApplicationUser user)
    {
        user.IsActive = false;
        user.UpdatedAt = DateTime.Now;

        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var errors = string.Join("; ", updateResult.Errors.Select(e => e.Description));
            _logger.LogWarning("Erro ao desativar os dados do usuário {UserId}: {Errors}", user.Id, errors);

            IdentityOperationErrorsException.ThrowIfErrors($"Falha ao desativar a conta do usuário '{user.Id}': {errors})");
        }

        _logger.LogInformation("Conta do usuário {UserId} desativada com sucesso.", user.Id);

        // invalida os tokens ativos, forçando o logout em dispositivos conectados.
        await _userManager.UpdateSecurityStampAsync(user);

        return true;
    }
}