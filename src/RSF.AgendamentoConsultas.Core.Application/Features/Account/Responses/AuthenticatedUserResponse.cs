using RSF.AgendamentoConsultas.Core.Domain.Models;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;

public class Credentials(string token, string? refreshToken = null)
{
    public string Token { get; private set; } = token;
    public string? RefreshToken { get; private set; } = refreshToken;
}
public class AuthenticatedUserResponse
{
    public Credentials Credentials { get; private set; }
    public UsuarioAutenticadoModel Usuario { get; private set; }

    public AuthenticatedUserResponse(Credentials credentials, UsuarioAutenticadoModel usuario)
    {
        Usuario = usuario;
        Credentials = credentials;
    }
    public AuthenticatedUserResponse(UsuarioAutenticadoModel usuario)
    {
        Credentials = default!;
        Usuario = usuario;
    }
}