using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterPaciente;

public record RegisterPacienteRequest(
    string NomeCompleto,
    string CPF,
    string Username,
    string Email,
    string Telefone,
    string Genero,
    DateTime DataNascimento,
    string Password,
    string ConfirmPassword) : IRequest<Result<AuthenticatedUserResponse>>;



