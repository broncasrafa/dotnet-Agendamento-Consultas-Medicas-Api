using Newtonsoft.Json.Linq;

using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Core.Domain.Models;

public class UsuarioAutenticadoModel
{
    public string Token { get; set; }
    public string Id { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    public static UsuarioAutenticadoModel MapFromEntity(ApplicationUser user)
        => user is null ? default! : new UsuarioAutenticadoModel
        {
            Id = user.Id,
            Nome = user.Nome,
            CPF = user.CPF,
            Username = user.UserName,
            Email = user.Email,
            Telefone = user.PhoneNumber,
            IsActive = user.IsActive.Value,
            CreatedAt = user.CreatedAt.Value
        };
    public static UsuarioAutenticadoModel MapFromEntity(ApplicationUser user, string token)
    {
        if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));

        return new UsuarioAutenticadoModel
        {
            Token = token,
            Id = user.Id,
            Nome = user.Nome,
            CPF = user.CPF,
            Username = user.UserName,
            Email = user.Email,
            Telefone = user.PhoneNumber,
            IsActive = user.IsActive.Value,
            CreatedAt = user.CreatedAt.Value
        };
    }
}