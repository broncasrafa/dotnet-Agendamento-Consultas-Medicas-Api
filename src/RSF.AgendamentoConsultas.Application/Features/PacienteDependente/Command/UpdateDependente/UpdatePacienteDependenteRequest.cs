using MediatR;

using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.UpdateDependente;

public record UpdatePacienteDependenteRequest(
    int DependenteId,
    string CPF,
    string Email,
    string NomeCompleto,
    string Telefone,
    DateTime DataNascimento,
    string Genero,
    int PacientePrincipalId) : IRequest<Result<bool>>;