using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;

namespace RSF.AgendamentoConsultas.Infra.Identity.AccountManager;

public class AccountManagerService : IAccountManagerService
{
    private readonly ILogger<AccountManagerService> _logger;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountManagerService(
        ILogger<AccountManagerService> logger,
        IJwtTokenService jwtTokenService,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;
    }

    
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

    public async Task<UsuarioAutenticadoModel> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        NotFoundException.ThrowIfNull(user, $"Usuário com o e-mail '{email}' não encontrado");

        var isValidCredentials = await _userManager.CheckPasswordAsync(user, password);
        InvalidCredentialsException.ThrowIfNotValid(isValidCredentials);

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);

        var token = _jwtTokenService.GenerateTokenJwt(user, roleClaims, userClaims);

        var response = UsuarioAutenticadoModel.MapFromEntity(user, token);

        return response;
    }

    public async Task<UsuarioAutenticadoModel> RegisterAsync(string nomeCompleto, string documento, string username, string email, string telefone, string genero, string password, ETipoPerfilAcesso tipoAcesso)
    {
        var user = await _userManager.FindByEmailAsync(email);
        AlreadyExistsException.ThrowIfExists(user, "Usuário já cadastrado");

        var newUser = new ApplicationUser
        {
            NomeCompleto = nomeCompleto,
            UserName = username,
            Documento = documento,
            Email = email,
            Genero = genero,
            PhoneNumber = telefone.RemoverFormatacaoSomenteNumeros(),
            IsActive = true
        };

        var result = await _userManager.CreateAsync(newUser, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, tipoAcesso.ToString());

            return UsuarioAutenticadoModel.MapFromEntity(newUser);
        }

        throw new InvalidOperationException($"Erro ao criar usuário: {string.Join(", ", result.Errors.Select(x => x.Description))}");
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
}