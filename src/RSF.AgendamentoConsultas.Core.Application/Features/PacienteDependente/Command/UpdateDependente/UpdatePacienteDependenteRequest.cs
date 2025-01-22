using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.UpdateDependente;

public record UpdatePacienteDependenteRequest(
    int DependenteId,
    int PacientePrincipalId,
    string CPF,
    string Email,
    string NomeCompleto,
    string Telefone,
    string Genero,
    DateTime DataNascimento) : IRequest<Result<bool>>;