using System.Linq.Expressions;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;

public interface IAccountManagerService
{
    Task<UsuarioAutenticadoModel> LoginAsync(string email, string password);
    Task<UsuarioAutenticadoModel> RegisterAsync(string nomeCompleto, string documento, string username, string email, string telefone, string genero, string password, ETipoPerfilAcesso tipoAcesso);
    Task<UsuarioAutenticadoModel> CheckIfAlreadyExistsByFilterAsync(Expression<Func<ApplicationUser, bool>> filter);
    
}