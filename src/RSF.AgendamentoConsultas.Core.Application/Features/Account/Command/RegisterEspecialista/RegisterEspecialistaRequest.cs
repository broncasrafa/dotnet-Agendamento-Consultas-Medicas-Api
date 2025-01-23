using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterEspecialista;

public record RegisterEspecialistaRequest(
    string NomeCompleto,
    string Username,
    string Email,
    string Genero,
    string Licenca,
    string Telefone,
    string Password,
    string ConfirmPassword) : IRequest<Result<AuthenticatedUserResponse>>;



