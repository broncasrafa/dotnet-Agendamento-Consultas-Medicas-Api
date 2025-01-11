using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.CreateDependente;

public class CreatePacienteDependenteRequest : IRequest<Result<PacienteDependenteResponse>>
{
    public int PacientePrincipalId { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public string Genero { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }
    public string Telefone { get; set; }
}