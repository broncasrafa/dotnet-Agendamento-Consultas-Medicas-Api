using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Models;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;

public class Credentials(string token, string? refreshToken = null)
{
    public string Token { get; private set; } = token;
    public string? RefreshToken { get; private set; } = refreshToken;
}
public class AuthenticatedUserResponse
{
    public UsuarioAutenticadoModel? Usuario { get; private set; }
    public PacienteResponse? Paciente { get; private set; }
    public EspecialistaResponse? Profissional { get; private set; }
    public Credentials Credentials { get; private set; }

    public AuthenticatedUserResponse(
        Credentials credentials, 
        PacienteResponse? paciente = null, 
        EspecialistaResponse? profissional = null, 
        UsuarioAutenticadoModel? usuario = null)
    {
        Credentials = credentials;
        Paciente = paciente ?? null;
        Usuario = usuario ?? null;
        Profissional = profissional ?? null;
    }

    public AuthenticatedUserResponse(
        PacienteResponse? paciente = null,
        EspecialistaResponse? profissional = null,
        UsuarioAutenticadoModel? usuario = null)
    {
        Credentials = default!;
        Paciente = paciente ?? null;
        Usuario = usuario ?? null;
        Profissional = profissional ?? null;
    }
}